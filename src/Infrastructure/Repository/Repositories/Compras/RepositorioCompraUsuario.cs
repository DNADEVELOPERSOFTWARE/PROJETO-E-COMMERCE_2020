using Domain.Interfaces.InterfaceCompra;
using Entity.Entities.Compras;
using Entity.Entities.Enuns;
using Entity.Entities.Produtos;
using Infrastructure.Configurations.Context;
using Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;   


namespace Infrastructure.Repository.Repositories.Compras
{
    public class RepositorioCompraUsuario : RepositorioGenerico<CompraUsuario>, ICompraUsuario
    {
        private readonly DbContextOptions<BaseContexto> _OptionsBuilder;
        public RepositorioCompraUsuario()
        {
            _OptionsBuilder = new DbContextOptions<BaseContexto>();
        }

        public async Task<bool> ConfirmaCompraCarrinhoUsuario(string userId)
        {
            try
            {
                using (var banco = new BaseContexto(_OptionsBuilder))
                {
                    var compraUsuario = new CompraUsuario();
                    compraUsuario.ListaProdutos = new List<Produto>();

                    var produtoCarrinho = await (from p in banco.Produto
                                                 join c in banco.CompraUsuario on p.Id equals c.ProdutoId
                                                 where c.UserId.Equals(userId) && c.Estado == EstadoCompra.Produto_Carrinho
                                                 select c).AsNoTracking().ToListAsync();

                    produtoCarrinho.ForEach(p =>
                    {
                        compraUsuario.CompraId = p.CompraId;
                        p.Estado = EstadoCompra.Produto_Comprado;
                    });

                    var compra = await banco.Compra.AsNoTracking().FirstOrDefaultAsync(c => c.Id == compraUsuario.CompraId);
                    if(compra != null)
                    {
                        compra.Estado = EstadoCompra.Produto_Comprado;
                    }

                    banco.Update(compra);
                    banco.UpdateRange(produtoCarrinho);
                    await banco.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception erro)
            {

                return false;
            }
        }

        public async Task<List<CompraUsuario>> MinhasCompradosPorEstado(string userId, EstadoCompra estado)
        {
            var retorno = new List<CompraUsuario>();

            using (var banco = new BaseContexto(_OptionsBuilder))
            {
                var comprasUsuario = await banco.Compra.Where(co => co.Estado == estado && co.UserId.Equals(userId)).ToListAsync();

                foreach (var item in comprasUsuario)
                {
                    var compraUsuario = new CompraUsuario();
                    compraUsuario.ListaProdutos = new List<Produto>();

                    var produtosCarrinhoUsuario = await (from p in banco.Produto
                                                         join c in banco.CompraUsuario on p.Id equals c.ProdutoId
                                                         where c.UserId.Equals(userId) && c.Estado == estado && c.CompraId == item.Id
                                                         select new Produto
                                                         {
                                                             Id = p.Id,
                                                             Nome = p.Nome,
                                                             Descricao = p.Descricao,
                                                             Observacao = p.Observacao,
                                                             Valor = p.Valor,
                                                             QuantidadeCompra = c.QuantidadeCompra,
                                                             ProdutoCarrinhoId = c.Id,
                                                             Url = p.Url,
                                                             DataCompra = item.DataCompra
                                                         }).AsNoTracking().ToListAsync();


                    compraUsuario.ListaProdutos = produtosCarrinhoUsuario;
                    compraUsuario.ApplicationUser = await banco.ApplicationUsers.FirstOrDefaultAsync(u => u.Id.Equals(userId));
                    compraUsuario.QuantidadeProduto = produtosCarrinhoUsuario.Count();
                    compraUsuario.EnderecoCompleto = string.Concat(compraUsuario.ApplicationUser.Endereco, " - ", compraUsuario.ApplicationUser.ComplementoEndereco, " - CEP: ", compraUsuario.ApplicationUser.CEP);
                    compraUsuario.ValorTotal = produtosCarrinhoUsuario.Sum(v => v.Valor);
                    compraUsuario.Estado = estado;
                    compraUsuario.Id = item.Id;

                    retorno.Add(compraUsuario);
                }

                return retorno;
            }
        }

        public async Task<CompraUsuario> ProdutosCompradosPorEstado(string userId, EstadoCompra estado, int? compraId = null)
        {
            using (var banco = new BaseContexto(_OptionsBuilder))   
            {
                var compraUsuario = new CompraUsuario();
                compraUsuario.ListaProdutos = new List<Produto>();

                var produtoCarrinhoUsuario = await (from p in banco.Produto
                                                    join c in banco.CompraUsuario on p.Id equals c.ProdutoId
                                                    join co in banco.Compra on c.CompraId equals co.Id
                                                    where c.UserId.Equals(userId) && c.Estado == estado && co.UserId.Equals(userId) && c.Estado == estado  && (compraId == null || co.Id == compraId)                                  
                                                    select new Produto
                                                    {
                                                        Id = p.Id,
                                                        Nome = p.Nome,
                                                        Descricao = p.Descricao,
                                                        Observacao = p.Observacao,
                                                        Valor = p.Valor,
                                                        QuantidadeCompra = c.QuantidadeCompra,
                                                        ProdutoCarrinhoId = c.Id,
                                                        Url = p.Url,
                                                        DataCompra = co.DataCompra
                                                    }).AsNoTracking().ToListAsync();

                compraUsuario.ListaProdutos = produtoCarrinhoUsuario;
                compraUsuario.ApplicationUser = await banco.ApplicationUsers.FirstOrDefaultAsync(u => u.Id.Equals(userId));
                compraUsuario.QuantidadeProduto = produtoCarrinhoUsuario.Count();
                compraUsuario.EnderecoCompleto = string.Concat(compraUsuario.ApplicationUser.Endereco, " - ", compraUsuario.ApplicationUser.ComplementoEndereco, " - CEP: ", compraUsuario.ApplicationUser.CEP);
                compraUsuario.ValorTotal = produtoCarrinhoUsuario.Sum(v => v.Valor);
                compraUsuario.Estado = estado;
                return compraUsuario;
            }
        }

        public async Task<int> QuantidadeProdutoCarrinhoUsuario(string userId)
        {
            using (var banco = new BaseContexto(_OptionsBuilder))
            {
                return await banco.CompraUsuario.CountAsync(c => c.UserId.Equals(userId) && c.Estado == EstadoCompra.Produto_Carrinho);
            }
        }
 
    }
}
