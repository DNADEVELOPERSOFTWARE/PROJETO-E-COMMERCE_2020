using Application.Interfaces.ISistemas;
using Domain.Interfaces.InterfaceSistema.LogsSitema;
using Entity.Entities.Sistema;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.OpenApp.AppSistemas
{
    public class AppLogSistema : ILogSistemaApp
    {
        private readonly ILogSistema _iLogSistema;
        public AppLogSistema(ILogSistema iLogSistema)
        {
            _iLogSistema = iLogSistema;
        }

        //======------Métodos CRUD------======//
        public async Task Add(LogSistema Objeto)
        {
            await _iLogSistema.Add(Objeto);
        }
        public async Task Update(LogSistema Objeto)
        {
            await _iLogSistema.Update(Objeto);
        }
        public async Task Delete(LogSistema Objeto)
        {
            await _iLogSistema.Delete(Objeto);
        }

        //======------Métodos de pesquisa---===//
        public async Task<LogSistema> GetEntityById(int Id)
        {
            return await _iLogSistema.GetEntityById(Id);
        }

        public async Task<List<LogSistema>> List()
        {
            return await _iLogSistema.List()
        }

       
    }
}
