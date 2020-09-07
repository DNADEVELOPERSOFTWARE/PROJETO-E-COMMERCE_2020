using Domain.Interfaces.InterfaceCompraUsuario;
using Domain.Interfaces.InterfaceServico;
using Entity.Entities.Compras;
using Entity.Entities.Enuns;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace Domain.Services.ServiceComprasUsuarios
{
    public class ServiceCompraUuuario : IServiceCompraUsuario
    {
        private readonly ICompraUsuario _iCompraUsuario;
        public ServiceCompraUuuario(ICompraUsuario iCompraUsuario)
        {
            _iCompraUsuario = iCompraUsuario;
        }
        public async Task<CompraUsuario> CarrinhoCompras(string userId)
        {
            return await _iCompraUsuario.ProdutosCompradosPorEstado(userId, EstadoCompra.Produto_Carrinho);
        }

        public async Task<CompraUsuario> ProdutosComprados(string userId)
        {
            return await _iCompraUsuario.ProdutosCompradosPorEstado(userId, EstadoCompra.Produto_Comprado);
        }
    }
}
