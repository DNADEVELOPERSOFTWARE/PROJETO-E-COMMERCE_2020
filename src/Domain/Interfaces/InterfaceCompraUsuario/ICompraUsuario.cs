using Domain.Interfaces.Generic;
using Entity.Entities.Compras;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceCompraUsuario
{
    public interface ICompraUsuario : IGenerica<CompraUsuario>
    {
        //Método que indica a quantidade de itens no carrinho

        public Task<int> QuantidadeProdutoCarrinhoUsuario(string userId);
    }
}
