using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Generic
{
    public interface IGenericaApp<T> where T : class
    {
        Task Add(T Objeto);
        Task Update(T Objeto);
        Task Delete(T Objeto);
        Task<T> GetEntityById(int Id);
        Task<List<T>> List();
    }
}
