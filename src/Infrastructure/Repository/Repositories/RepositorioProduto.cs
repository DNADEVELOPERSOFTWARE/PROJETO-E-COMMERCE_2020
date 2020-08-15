using Domain.Interfaces.InterfaceProduto;
using Entity.Entities.Produtos;
using Infrastructure.Configurations.Context;
using Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class RepositorioProduto : RepositorioGenerico<Produto>, IProduto
    {
        private readonly DbContextOptions<BaseContexto> _optionsBulder;
        public RepositorioProduto()
        {
            _optionsBulder = new DbContextOptions<BaseContexto>();
        }

        public async Task<List<Produto>> ListarProdutos(Expression<Func<Produto, bool>> exProduto)
        {
            using (var banco = new BaseContexto(_optionsBulder))
            {
                return await banco.Produto.Where(exProduto).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Produto>> ListarProdutoUsuario(string userId)
        {
            using (var banco = new BaseContexto(_optionsBulder))
            {
                return await banco.Produto.Where(p => p.UserId == userId).AsNoTracking().ToListAsync();
            }
        }
    }
}
