using Domain.Interfaces.InterfaceCompraUsuario;
using Entity.Entities.Compras;
using Infrastructure.Repository.Generic;

namespace Infrastructure.Repository.Repositories
{
    public class RepositorioCompraUsuario : RepositorioGenerico<CompraUsuario>, ICompraUsuario
    {
    }
}
