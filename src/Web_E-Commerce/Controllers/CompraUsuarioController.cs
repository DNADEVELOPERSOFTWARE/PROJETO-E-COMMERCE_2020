using Application.Interfaces.IComprasUsuarios;
using Entity.Entities.Compras;
using Entity.Entities.Enuns;
using Entity.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Web_E_Commerce.Controllers
{
    public class CompraUsuarioController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICompraUsuarioApp _iCompraUsuarioApp;

        public CompraUsuarioController(UserManager<ApplicationUser> userManager, ICompraUsuarioApp iCompraUsuarioApp)
        {
            _userManager = userManager;
            _iCompraUsuarioApp = iCompraUsuarioApp;
        }


        [HttpPost("/api/AdicionarProdutosCarrinho")]
        public async Task<JsonResult> AdicionarProdutosCarrinho(string id, string nome, string qtd)
        {
            //Pega o id do usuário logado
            var usuario = await _userManager.GetUserAsync(User);
            // Verifica se o usuário é valido
            if (usuario != null)
            {
                await _iCompraUsuarioApp.Add(new CompraUsuario
                {
                    ProdutoId = Convert.ToInt32(id),
                    QuantidadeCompra = Convert.ToInt32(qtd),
                    Estado = EstadoCompra.Produto_Caminho,
                    UserId = usuario.Id
                });
                return Json(new { sucesso = true });
            }
            return Json(new { sucesso = false });
        }
    }
}
