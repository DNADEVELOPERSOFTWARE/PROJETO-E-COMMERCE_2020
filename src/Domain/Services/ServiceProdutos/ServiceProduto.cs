using Domain.Interfaces.InterfaceProduto;
using Domain.Interfaces.InterfaceServico;
using Entity.Entities.Produtos;
using System;
using System.Threading.Tasks;

namespace Domain.Services.ServiceProdutos
{
    /// <summary>
    /// Validações da regra de negócio de produto
    /// </summary>
    public class ServiceProduto : IServiceProduto
    {
        private readonly IProduto _IProduto;
        public ServiceProduto(IProduto IProduto)
        {
            _IProduto = IProduto;
        }

        //Válidações de campos em branco
        public async Task AddProduto(Produto produto)
        {
            var validaNome = produto.ValidarPropriedadeString(produto.Nome, "Nome");

            var validaValor = produto.ValidarPropriedadeDecimal(produto.Valor, "Valor");

            var validaQntdEstoque = produto.ValidarPropriedadeInt(produto.QuantidadeEstoque, "QuantidadeEstoque");

            if (validaNome && validaValor && validaQntdEstoque)
            {
                produto.DataCadastro = DateTime.Now;
                produto.DataAlteracao = DateTime.Now;   
                produto.Estado = true;
                await _IProduto.Add(produto);
            }
        }

        public async Task UpdateProduto(Produto produto)
        {
            var validaNome = produto.ValidarPropriedadeString(produto.Nome, "Nome");

            var validaValor = produto.ValidarPropriedadeDecimal(produto.Valor, "Valor");

            var validaQntdEstoque = produto.ValidarPropriedadeInt(produto.QuantidadeEstoque, "QuantidadeEstoque");

            if (validaNome && validaValor && validaQntdEstoque)
            {
                produto.DataAlteracao = DateTime.Now;

                await _IProduto.Update(produto);
            }
        }
    }
}
