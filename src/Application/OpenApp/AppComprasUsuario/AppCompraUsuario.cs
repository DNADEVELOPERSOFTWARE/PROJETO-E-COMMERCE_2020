using Application.Interfaces.IComprasUsuarios;
using Domain.Interfaces.InterfaceCompraUsuario;
using Entity.Entities.Compras;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.OpenApp.AppComprasUsuario
{
    public class AppCompraUsuario : ICompraUsuarioApp
    {
        private readonly ICompraUsuario _iCompraUsuario;

        public AppCompraUsuario(ICompraUsuario iCompraUsuario)
        {
            _iCompraUsuario = iCompraUsuario;
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
        #endregion
    }
}
