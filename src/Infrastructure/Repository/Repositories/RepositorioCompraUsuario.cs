 using Domain.Interfaces.InterfaceCompraUsuario;
using Entity.Entities.Compras;
using Entity.Entities.Enuns;
using Infrastructure.Configurations.Context;
using Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class RepositorioCompraUsuario : RepositorioGenerico<CompraUsuario>, ICompraUsuario
    {
        private readonly DbContextOptions<BaseContexto> _OptiosBuilder;
        public RepositorioCompraUsuario()
        {
            _OptiosBuilder = new DbContextOptions<BaseContexto>();
        }

        public async Task<int> QuantidadeProdutoCarrinhoUsuario(string userId)
        {
            using (var banco = new BaseContexto(_OptiosBuilder))
            {
                return await banco.CompraUsuario.CountAsync(c => c.UserId.Equals(userId) && c.Estado == EstadoCompra.Produto_Carrinho);
            }
        }
    }
}
