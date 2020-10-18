using Domain.Interfaces.Generic;
using Entity.Entities.Compras;
using Entity.Entities.Enuns;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceCompra
{
    public interface ICompra : IGenerica<Compra>
    {
        public Task<Compra> CompraPorEstado(string userId, EstadoCompra estado);
    }
}
