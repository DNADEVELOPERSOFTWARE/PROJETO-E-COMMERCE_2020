using Domain.Interfaces.InterfaceProduto;
using Entity.Entities.Produtos;
using Infrastructure.Repository.Generic;

namespace Infrastructure.Repository.Repositories
{
    public class RepositorioProduto : RepositorioGenerico<Produto>, IProduto
    {
    }
}
