using Domain.Interfaces.Generic;
using Entity.Entities.Compras;
using Entity.Entities.Enuns;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceCompra
{
    public interface ICompraUsuario : IGenerica<CompraUsuario>
    {
        //Método que indica a quantidade de itens no carrinho

        public Task<int> QuantidadeProdutoCarrinhoUsuario(string userId);

        public Task<CompraUsuario> ProdutosCompradosPorEstado(string userId, EstadoCompra estado, int? compraId = null);

        public Task<List<CompraUsuario>> MinhasCompradosPorEstado(string userId, EstadoCompra estado);

        public Task<bool> ConfirmaCompraCarrinhoUsuario(string userId);
    }
}
