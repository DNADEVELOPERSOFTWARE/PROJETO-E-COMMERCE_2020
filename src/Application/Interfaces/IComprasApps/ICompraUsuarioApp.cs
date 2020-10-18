using Application.Interfaces.Generic;
using Entity.Entities.Compras;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.IComprasApps
{
    public interface ICompraUsuarioApp : IGenericaApp<CompraUsuario>
    {
        public Task<int> QuantidadeProdutoCarrinhoUsuario(string userId);

        public Task<CompraUsuario> CarrinhoCompras(string userId);

        public Task<CompraUsuario> ProdutosComprados(string userId, int? compraId = null);

        public Task<bool> ConfirmarCompraCarrinhoUsuario(string userId);

        public Task<List<CompraUsuario>> MinhasCompras(string userId);

        public Task AdicionarProdutoCarrinho(string userId, CompraUsuario compraUsuario);
    }
}
