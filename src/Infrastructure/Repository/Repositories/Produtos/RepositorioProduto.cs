using Domain.Interfaces.InterfaceProduto;
using Entity.Entities.Enuns;
using Entity.Entities.Produtos;
using Infrastructure.Configurations.Context;
using Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories.Produtos
{
    public class RepositorioProduto : RepositorioGenerico<Produto>, IProduto
    {
        private readonly DbContextOptions<BaseContexto> _optionsBulder;
        public RepositorioProduto()
        {
            _optionsBulder = new DbContextOptions<BaseContexto>();
        }

        #region MÉTODOS CUSTUMIZADOS
       
        public async Task<List<Produto>> ListarProdutoCarrinhoUsuario(string userId)
        {
            using (var banco = new BaseContexto(_optionsBulder))
            {
                var produtoCarrinhoUsuario = await (from p in banco.Produto
                                                    join c in banco.CompraUsuario on p.Id equals c.ProdutoId
                                                    join co in banco.Compra on c.CompraId equals co.Id
                                                    where c.UserId.Equals(userId) && c.Estado == EstadoCompra.Produto_Carrinho
                                                    select new Produto
                                                    {
                                                        Id = p.Id,
                                                        Nome = p.Nome,
                                                        Descricao = p.Descricao,
                                                        Observacao = p.Observacao,
                                                        Valor = p.Valor,
                                                        QuantidadeCompra = c.QuantidadeCompra,
                                                        ProdutoCarrinhoId = c.Id,
                                                        Url = p.Url,
                                                        DataCompra = co.DataCompra  

                                                    }).AsNoTracking().ToListAsync();

                return produtoCarrinhoUsuario;
            }
        }

        public async Task<Produto> ObterProdutoCarrinho(int IdProdutoCarrinho)
        {
            using (var banco = new BaseContexto(_optionsBulder))
            {
                var produtoCarrinhoUsuario = await (from p in banco.Produto
                                                    join c in banco.CompraUsuario on p.Id equals c.ProdutoId
                                                    where c.Id.Equals(IdProdutoCarrinho) && c.Estado == EstadoCompra.Produto_Carrinho
                                                    select new Produto
                                                    {
                                                        Id = p.Id,
                                                        Nome = p.Nome,
                                                        Descricao = p.Descricao,
                                                        Observacao = p.Observacao,
                                                        Valor = p.Valor,
                                                        QuantidadeCompra = c.QuantidadeCompra,
                                                        ProdutoCarrinhoId = c.Id,
                                                        Url =p.Url

                                                    }).AsNoTracking().FirstOrDefaultAsync();

                return produtoCarrinhoUsuario;
            }
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

        #endregion
    }
}
