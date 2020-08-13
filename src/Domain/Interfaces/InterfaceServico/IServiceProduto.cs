using Entity.Entities.Produtos;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceServico
{
    public interface IServiceProduto
    {
        Task AddProduto(Produto produto);
        Task UpdateProduto(Produto produto);
    }
}
