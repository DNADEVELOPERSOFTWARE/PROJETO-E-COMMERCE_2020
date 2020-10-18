using Domain.Interfaces.InterfaceCompra;
using Entity.Entities.Compras;
using Entity.Entities.Enuns;
using Infrastructure.Configurations.Context;
using Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories.Compras
{
    public class RepositorioCompra : RepositorioGenerico<Compra>, ICompra
    {
        private readonly DbContextOptions<BaseContexto> _optionBuilder;

        public RepositorioCompra()
        {
            _optionBuilder = new DbContextOptions<BaseContexto>();
        }

        public async Task<Compra> CompraPorEstado(string userId, EstadoCompra estado)
        {
            using (var banco = new BaseContexto(_optionBuilder))
            {
                return await banco.Compra.FirstOrDefaultAsync(c => c.Estado == estado && c.UserId == userId);
            }
        }
    }
}
