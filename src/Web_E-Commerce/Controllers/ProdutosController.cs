using Application.Interfaces.IComprasApps;
using Application.Interfaces.IProduto;
using Application.Interfaces.ISistemas;
using Entity.Entities.Enuns;
using Entity.Entities.Produtos;
using Entity.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;
using Web_E_Commerce.Models;

namespace Web_E_Commerce.Controllers
{
    [Authorize]
    [LogActionFilter]
    public class ProdutosController : BaseController
    {
        public readonly InterfaceProdutoApp _interfaceProdutoApp;
        private readonly ICompraUsuarioApp _iCompraUsuarioApp;
        private IWebHostEnvironment _environment;

        public ProdutosController(InterfaceProdutoApp interfaceProdutoApp,
            ILogger<ProdutosController> logger,
            UserManager<ApplicationUser> userManager, ICompraUsuarioApp iCompraUsuarioApp,
            ILogSistemaApp iLogSistemaApp,
            IWebHostEnvironment environment)
            :base(logger,userManager,iLogSistemaApp)
        {
            _interfaceProdutoApp = interfaceProdutoApp;
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
                await LogEcommerce(TipoLog.Informativo, produto);
            }
            catch(Exception erro)
            {
                await LogEcommerce(TipoLog.Erro, erro);
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
            catch(Exception erro)
            {
                await LogEcommerce(TipoLog.Erro, erro);
                return View("Edit", produto);
            }
            await LogEcommerce(TipoLog.Informativo, produto);
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

                await LogEcommerce(TipoLog.Informativo, deletarProduto);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception erro)
            {
                await LogEcommerce(TipoLog.Erro, erro);
                return View();
            }
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

                await LogEcommerce(TipoLog.Informativo, deletarProduto);

                return RedirectToAction(nameof(ListarProdutosCarrinhoUsuario));
            }
            catch(Exception erro)
            {
                await LogEcommerce(TipoLog.Erro, erro);
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

                await LogEcommerce(TipoLog.Erro, erro);
            }

        }
    }

}

