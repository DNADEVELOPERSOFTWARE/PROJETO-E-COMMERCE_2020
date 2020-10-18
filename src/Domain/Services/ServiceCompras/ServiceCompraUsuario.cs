using Domain.Interfaces.InterfaceCompra;
using Domain.Interfaces.InterfaceServico;
using Entity.Entities.Compras;
using Entity.Entities.Enuns;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services.ServiceCompras
{
    public class ServiceCompraUuuario : IServiceCompraUsuario
    {
        private readonly ICompraUsuario _iCompraUsuario;
        private readonly ICompra _iCompra;

        public ServiceCompraUuuario(ICompraUsuario iCompraUsuario, ICompra iCompra)
        {
            _iCompra = iCompra;
            _iCompraUsuario = iCompraUsuario;
        }

        public async Task AdicionarProdutoCarrinho(string userId, CompraUsuario compraUsuario)
        {
            var compra = await _iCompra.CompraPorEstado(userId, EstadoCompra.Produto_Carrinho);
            if(compra == null)
            {
                compra = new Compra
                {
                    UserId = userId,
                    Estado = EstadoCompra.Produto_Carrinho
                };
                await _iCompra.Add(compra);
            }

            if(compra.Id > 0)
            {
                compraUsuario.CompraId = compra.Id;
                await _iCompraUsuario.Add(compraUsuario);
            }
        }

        public async Task<CompraUsuario> CarrinhoCompras(string userId)
        {
            return await _iCompraUsuario.ProdutosCompradosPorEstado(userId, EstadoCompra.Produto_Carrinho);
        }

        public async Task<List<CompraUsuario>> MinhasCompras(string userId)
        {
            return await _iCompraUsuario.MinhasCompradosPorEstado(userId, EstadoCompra.Produto_Comprado);
        }

        public async Task<CompraUsuario> ProdutosComprados(string userId, int? compraId = null)
        {
            return await _iCompraUsuario.ProdutosCompradosPorEstado(userId, EstadoCompra.Produto_Comprado, compraId);
        }
    }
}
