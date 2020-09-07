using Domain.Interfaces.InterfaceCompraUsuario;
using Entity.Entities.Compras;
using Entity.Entities.Enuns;
using Infrastructure.Configurations.Context;
using Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Entity.Entities.Produtos;
using System;
using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Repository.Repositories
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
                        p.Estado = EstadoCompra.Produto_Comprado;
                    });

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

        public async Task<CompraUsuario> ProdutosCompradosPorEstado(string userId, EstadoCompra estado)
        {
            using (var banco = new BaseContexto(_OptionsBuilder))
            {
                var compraUsuario = new CompraUsuario();
                compraUsuario.ListaProdutos = new List<Produto>();

                var produtoCarrinhoUsuario = await (from p in banco.Produto
                                                    join c in banco.CompraUsuario on p.Id equals c.ProdutoId
                                                    where c.UserId.Equals(userId) && c.Estado == estado
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
                                                    }).AsNoTracking().ToListAsync();

                compraUsuario.ListaProdutos = produtoCarrinhoUsuario;
                compraUsuario.ApplicationUser = await banco.ApplicationUsers.FirstOrDefaultAsync(u => u.Id.Equals(userId));
                compraUsuario.QuantidadeProduto = produtoCarrinhoUsuario.Count();
                compraUsuario.EndercoCompleto = string.Concat(compraUsuario.ApplicationUser.Endereco, " - ", compraUsuario.ApplicationUser.ComplementoEndereco, " - CEP: ", compraUsuario.ApplicationUser.CEP);
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
