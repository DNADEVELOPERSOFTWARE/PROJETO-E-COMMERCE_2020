using Domain.Interfaces.InterfaceSistema.LogsSitema;
using Entity.Entities.Sistema;
using Infrastructure.Repository.Generic;

namespace Infrastructure.Repository.Repositories.Sistemas
{
    public class RepositorioLogSistema : RepositorioGenerico<LogSistema>, ILogSistema
    {
    }
}
