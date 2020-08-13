using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Generic
{
    public interface IGenerica<T> where T : class
    {
        Task Add(T Objeto);
        Task Update(T Objeto);
        Task Delete(T Objeto);
        Task<T> GetEntityById(int Id);
        Task<List<T>> List();
    }
}
