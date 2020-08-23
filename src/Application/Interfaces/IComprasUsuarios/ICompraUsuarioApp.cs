using Application.Interfaces.Generic;
using Entity.Entities.Compras;
using System.Threading.Tasks;

namespace Application.Interfaces.IComprasUsuarios
{
    public interface ICompraUsuarioApp : InterfaceGenericaApp<CompraUsuario>
    {
        public Task<int> QuantidadeProdutoCarrinhoUsuario(string userId);
    }
}
