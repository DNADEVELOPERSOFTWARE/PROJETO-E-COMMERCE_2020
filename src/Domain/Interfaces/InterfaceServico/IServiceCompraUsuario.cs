using Entity.Entities.Compras;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceServico
{
    public interface IServiceCompraUsuario
    {
        public Task<CompraUsuario> CarrinhoCompras(string userId);

        public Task<CompraUsuario> ProdutosComprados(string userId);
    }
}
