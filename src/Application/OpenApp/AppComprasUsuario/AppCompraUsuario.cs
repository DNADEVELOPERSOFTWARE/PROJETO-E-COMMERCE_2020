using Application.Interfaces.IComprasUsuarios;
using Domain.Interfaces.InterfaceCompraUsuario;
using Domain.Interfaces.InterfaceServico;
using Entity.Entities.Compras;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.OpenApp.AppComprasUsuario
{
    public class AppCompraUsuario : ICompraUsuarioApp
    {
        private readonly ICompraUsuario _iCompraUsuario;

        private readonly IServiceCompraUsuario _iServiceCompraUsuario;

        public AppCompraUsuario(ICompraUsuario iCompraUsuario, IServiceCompraUsuario iServiceCompraUsuario)
        {
            _iCompraUsuario = iCompraUsuario;
            _iServiceCompraUsuario = iServiceCompraUsuario;
        }

        public async Task Add(CompraUsuario Objeto)
        {
            await _iCompraUsuario.Add(Objeto);
        }

        public async Task Delete(CompraUsuario Objeto)
        {
            await _iCompraUsuario.Delete(Objeto);
        }

        public async Task<CompraUsuario> GetEntityById(int Id)
        {
            return await _iCompraUsuario.GetEntityById(Id);
        }

        public async Task<List<CompraUsuario>> List()
        {
            return await _iCompraUsuario.List();
        }

        public async Task Update(CompraUsuario Objeto)
        {
            await _iCompraUsuario.Update(Objeto);
        }

        #region MÉTODOS CUSTUMIZADOS
        public async Task<int> QuantidadeProdutoCarrinhoUsuario(string userId)
        {
            return await _iCompraUsuario.QuantidadeProdutoCarrinhoUsuario(userId);
        }

        public async Task<CompraUsuario> CarrinhoCompras(string userId)
        {
            return await _iServiceCompraUsuario.CarrinhoCompras(userId);
        }

        public async Task<CompraUsuario> ProdutosComprados(string userId)
        {
            return await _iServiceCompraUsuario.ProdutosComprados(userId);
        }

        public async Task<bool> ConfirmarCompraCarrinhoUsuario(string userId)
        {
            return await _iCompraUsuario.ConfirmaCompraCarrinhoUsuario(userId);
        }
        #endregion
    }
}
