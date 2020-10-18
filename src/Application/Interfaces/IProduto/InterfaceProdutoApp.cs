using Application.Interfaces.Generic;
using Entity.Entities.Produtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.IProduto
{
    public interface InterfaceProdutoApp : IGenericaApp<Produto>
    {
        Task AddProduto(Produto produto);
        Task UpdateProduto(Produto produto);

        Task<List<Produto>> ListarProdutoUsuario(string userId);

        Task<List<Produto>> ListaProdutoComEstoque(string descricao);

        Task<List<Produto>> ListarProdutoCarrinhoUsuario(string userId);

        Task<Produto> ObterProdutoCarrinho(int ProdutoCarrinhoId);
    }
}
