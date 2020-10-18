using System;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;
using Application.Interfaces.IComprasApps;
using Application.Interfaces.IProduto;
using Entity.Entities.Produtos;
using Entity.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Web_E_Commerce.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        public readonly UserManager<ApplicationUser> _userManager;

        public readonly InterfaceProdutoApp _interfaceProdutoApp;
        private readonly ICompraUsuarioApp _iCompraUsuarioApp;
        private IWebHostEnvironment _environment;

        public ProdutosController(InterfaceProdutoApp interfaceProdutoApp,
            UserManager<ApplicationUser> userManager, ICompraUsuarioApp iCompraUsuarioApp,
            IWebHostEnvironment environment)
        {
            _interfaceProdutoApp = interfaceProdutoApp;
            _userManager = userManager;
            _iCompraUsuarioApp = iCompraUsuarioApp;
            _environment = environment;
        }

        // GET: ProdutosController
        public async Task<IActionResult> Index()
        {
            var idUsuario = await RetornarIdUsuarioLogado();

            return View(await _interfaceProdutoApp.ListarProdutoUsuario(idUsuario));
        }

        // GET: ProdutosController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await _interfaceProdutoApp.GetEntityById(id));
        }

        // GET: ProdutosController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: ProdutosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto)
        {
            try
            {
                var idUsuario = await RetornarIdUsuarioLogado();

                produto.UserId = idUsuario;

                await _interfaceProdutoApp.AddProduto(produto);
                if (produto.Notificacoes.Any())
                {
                    foreach (var item in produto.Notificacoes)
                    {
                        ModelState.AddModelError(item.NomePropriedade, item.mensagem);
                    }
                    return View("Create", produto);
                }

                await SalvarImagemProduto(produto);
            }
            catch
            {
                return View("Create", produto);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutosController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _interfaceProdutoApp.GetEntityById(id));
        }

        // POST: ProdutosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Produto produto)
        {
            try
            {
                await _interfaceProdutoApp.UpdateProduto(produto);
                if (produto.Notificacoes.Any())
                {
                    foreach (var item in produto.Notificacoes)
                    {
                        ModelState.AddModelError(item.NomePropriedade, item.mensagem);
                    }
                    ViewBag.Alerta = true;
                    ViewBag.Mensagem = "Verifique ocorreu um erro";

                    return View("Edit", produto);
                }
            }
            catch
            {
                return View("Edit", produto);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutosController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _interfaceProdutoApp.GetEntityById(id));
        }

        // POST: ProdutosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Produto produto)
        {
            try
            {
                var deletarProduto = await _interfaceProdutoApp.GetEntityById(id);

                await _interfaceProdutoApp.Delete(deletarProduto);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private async Task<string> RetornarIdUsuarioLogado()
        {
            var idUsuario = await _userManager.GetUserAsync(User);

            return idUsuario.Id;
        }

        [AllowAnonymous]
        [HttpGet("/api/ListaProdutosComEstoque")]
        public async Task<JsonResult> ListaProdutosComEstoque(string descricao)
        {
            return Json(await _interfaceProdutoApp.ListaProdutoComEstoque(descricao));
        }

        public async Task<IActionResult> ListarProdutosCarrinhoUsuario()
        {
            var idUsuario = await RetornarIdUsuarioLogado();

            return View(await _interfaceProdutoApp.ListarProdutoCarrinhoUsuario(idUsuario));
        }

        // GET: ProdutosController/Delete/5
        public async Task<IActionResult> RemoverCarrinho(int id)
        {
            return View(await _interfaceProdutoApp.ObterProdutoCarrinho(id));
        }

        // POST: ProdutosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverCarrinho(int id, Produto produto)
        {
            try
            {
                var deletarProduto = await _iCompraUsuarioApp.GetEntityById(id);

                await _iCompraUsuarioApp.Delete(deletarProduto);

                return RedirectToAction(nameof(ListarProdutosCarrinhoUsuario));
            }
            catch
            {
                return View();
            }
        }


        public async Task SalvarImagemProduto(Produto produtoTela)
        {
            try
            {
                var produto = await _interfaceProdutoApp.GetEntityById(produtoTela.Id);
                if (produtoTela.Imagem != null)
                {
                    var webRoot = _environment.WebRootPath;
                    var permissionSet = new PermissionSet(PermissionState.Unrestricted);
                    var writePermission = new FileIOPermission(FileIOPermissionAccess.Append, string.Concat(webRoot, "/imgProdutos"));
                    permissionSet.AddPermission(writePermission);

                    var Extension = System.IO.Path.GetExtension(produtoTela.Imagem.FileName);
                    var NomeArquivo = string.Concat(produto.Id.ToString(), Extension);

                    var diretorioArquivosSalvar = string.Concat(webRoot, "\\imgProdutos\\", NomeArquivo);

                    produtoTela.Imagem.CopyTo(new FileStream(diretorioArquivosSalvar, FileMode.Create));

                    produto.Url = string.Concat("https://localhost:5001", "/imgProdutos/", NomeArquivo);


                    await _interfaceProdutoApp.UpdateProduto(produto);
                }
            }
            catch (Exception erro)
            {


            }

        }
    }

}

