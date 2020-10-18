using Entity.Entities.Produtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceServico
{
    public interface IServiceProduto
    {
        Task AddProduto(Produto produto);

        Task UpdateProduto(Produto produto);

        Task<List<Produto>> ListarProdutosComEstoque(string descricao);
    }
}
