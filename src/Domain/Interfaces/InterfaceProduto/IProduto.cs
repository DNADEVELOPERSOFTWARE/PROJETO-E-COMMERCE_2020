using Domain.Interfaces.Generic;
using Entity.Entities.Produtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceProduto
{
    public interface IProduto : IGenerica<Produto>
    {
        Task<List<Produto>> ListarProdutoUsuario(string userId);     
    }
}
