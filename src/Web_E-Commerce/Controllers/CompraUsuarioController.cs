﻿using Application.Interfaces.IComprasUsuarios;
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

        public async Task<IActionResult> FinalizarCompras()
        {
            var usuario = await _userManager.GetUserAsync(User);
            var compraUauasrio = await _iCompraUsuarioApp.CarrinhoCompras(usuario.Id);
            return View(compraUauasrio);
        }

        public async Task<IActionResult> MinhasCompras(bool mensagem = false)
        {
            var usuario = await _userManager.GetUserAsync(User);
            var compraUauasrio = await _iCompraUsuarioApp.ProdutosComprados(usuario.Id);

            if (mensagem)
            {
                ViewBag.Sucesso = true;
                ViewBag.Mensagem = "Compra efetivada com sucesso pague o boleto para garantir seu pedido1";
            }
            return View(compraUauasrio);
        }

        public async Task<IActionResult> ConfirmaCompra(bool mensagem = false)
        {
            var usuario = await _userManager.GetUserAsync(User);
            var sucesso = await _iCompraUsuarioApp.ConfirmarCompraCarrinhoUsuario(usuario.Id);

            if (sucesso)
            {
                return RedirectToAction("MinhasCompras", new { mensagem = true });
            }
            else
            return RedirectToAction("FinalizarCompras");
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
                    Estado = EstadoCompra.Produto_Carrinho,
                    UserId = usuario.Id
                });
                return Json(new { sucesso = true });
            }
            return Json(new { sucesso = false });
        }

        [HttpGet("/api/QuantidadeProdutoCarrinho")]
        public async Task<JsonResult> QuantidadeProdutoCarrinho()
        {
            //Pega o id do usuário logado
            var usuario = await _userManager.GetUserAsync(User);
           
            var qtd = 0;

            // Verifica se o usuário é valido
            if (usuario != null)
            {
               qtd = await _iCompraUsuarioApp.QuantidadeProdutoCarrinhoUsuario(usuario.Id);
                return Json(new {sucesso = true, qtd = qtd });
            }
            return Json(new {sucesso = false, qtd = qtd });
        }
    }
}
