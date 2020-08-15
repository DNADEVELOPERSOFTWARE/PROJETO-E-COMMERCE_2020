using Application.Interfaces.Generic;
using Entity.Entities.Produtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.IProduto
{
    public interface InterfaceProdutoApp : InterfaceGenericaApp<Produto>
    {
        Task AddProduto(Produto produto);
        Task UpdateProduto(Produto produto);

        Task<List<Produto>> ListarProdutoUsuario(string userId);

        Task<List<Produto>> ListaProdutoComEstoque();
    }
}
