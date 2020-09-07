using Application.Interfaces.Generic;
using Entity.Entities.Compras;
using System.Threading.Tasks;

namespace Application.Interfaces.IComprasUsuarios
{
    public interface ICompraUsuarioApp : InterfaceGenericaApp<CompraUsuario>
    {
        public Task<int> QuantidadeProdutoCarrinhoUsuario(string userId);

        public Task<CompraUsuario> CarrinhoCompras(string userId);

        public Task<CompraUsuario> ProdutosComprados(string userId);

        public Task<bool> ConfirmarCompraCarrinhoUsuario(string userId);
    }
}
