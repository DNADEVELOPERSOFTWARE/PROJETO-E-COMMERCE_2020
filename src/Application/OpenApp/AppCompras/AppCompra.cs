using Application.Interfaces.IComprasApps;
using Domain.Interfaces.InterfaceCompra;
using Entity.Entities.Compras;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.OpenApp.AppCompras
{
    public class AppCompra : ICompraApp
    {
        private readonly ICompra _iCompra;

        public AppCompra(ICompra iCompra)
        {
            _iCompra = iCompra;
        }
         //======-------Métodos CRUD------======//
        public async Task Add(Compra Objeto)
        {
            await _iCompra.Add(Objeto);
        }

        public async Task Update(Compra Objeto)
        {
            await _iCompra.Update(Objeto);
        }

        public async Task Delete(Compra Objeto)
        {
            await _iCompra.Delete(Objeto);
        }

        //======------Metodos de pesquisas---===//
        public async Task<Compra> GetEntityById(int Id)
        {
            return await _iCompra.GetEntityById(Id);
        }

        public async Task<List<Compra>> List()
        {
            return await _iCompra.List();
        }

    }
}
