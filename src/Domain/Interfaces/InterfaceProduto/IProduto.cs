using Domain.Interfaces.Generic;
using Entity.Entities.Produtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceProduto
{
    public interface IProduto : IGenerica<Produto>
    {
        Task<List<Produto>> ListarProdutoUsuario(string userId);

        Task<List<Produto>> ListarProdutos(Expression<Func<Produto, bool>> exProduto);

        Task<List<Produto>> ListarProdutoCarrinhoUsuario(string userId);

        Task<Produto> ObterProdutoCarrinho(int ProdutoCarrinhoId);
    }
}
