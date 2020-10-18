using Domain.Interfaces.InterfaceProduto;
using Domain.Interfaces.InterfaceServico;
using Domain.Services.ServiceProdutos;
using Entity.Entities.Produtos;
using Infrastructure.Repository.Repositories.Produtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTestProjectEccomerce
{
    [TestClass]
    public class UnitTestEcommerce
    {
        [TestMethod]
       public async Task AdicionaProdutoComSucesso()
        {
            try
            {
                IProduto iProduto = new RepositorioProduto();
                IServiceProduto iServiceProduto = new ServiceProduto(iProduto);

                var produto = new Produto
                {
                    UserId = "5871002e-8139-4313-959f-bd077c914b3f",
                    Nome = string.Concat("Produto Fake para teste", DateTime.Now.ToString()),
                    Descricao = string.Concat("Descricao fake para Teste", DateTime.Now.ToString()),
                    Observacao = string.Concat("Observção fake para Teste", DateTime.Now.ToString()),
                    Valor = 20,
                    QuantidadeEstoque = 10,
                };
                await iServiceProduto.AddProduto(produto);

                Assert.IsFalse(produto.Notificacoes.Any());
            }
            catch (Exception)
            {

                Assert.Fail();
            }
           
        }

        [TestMethod]
        public async Task AdicionarProdutoComValidacaoCampoObrigatorio()
        {
            try
            {
                IProduto iProduto = new RepositorioProduto();
                IServiceProduto iServiceProduto = new ServiceProduto(iProduto);

                var produto = new Produto
                {
                    
                };
                await iServiceProduto.AddProduto(produto);

                Assert.IsTrue(produto.Notificacoes.Any());
            }
            catch (Exception)
            {

                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task ListaPrdutoUsuario()
        {
            try
            {
                IProduto iProduto = new RepositorioProduto();

                var listaProduto = await iProduto.ListarProdutoUsuario("5871002e-8139-4313-959f-bd077c914b3f");

                Assert.IsTrue(listaProduto.Any());
            }
            catch (Exception)
            {

                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task ObterEntidadePorId()
        {
            try
            {
                IProduto iProduto = new RepositorioProduto();

                var listaProduto = await iProduto.ListarProdutoUsuario("5871002e-8139-4313-959f-bd077c914b3f");

                var produto = await iProduto.GetEntityById(listaProduto.LastOrDefault().Id);
                Assert.IsTrue(produto != null);
            }
            catch (Exception)
            {

                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task Deletar()
        {
            try
            {
                IProduto iProduto = new RepositorioProduto();

                var listaProduto = await iProduto.ListarProdutoUsuario("5871002e-8139-4313-959f-bd077c914b3f");

                var produto = await iProduto.GetEntityById(listaProduto.LastOrDefault().Id);
                var ultimoProdto = listaProduto.LastOrDefault();

                await iProduto.Delete(ultimoProdto);

                Assert.IsTrue(true);
            }
            catch (Exception)
            {

                Assert.Fail();
            }
        }
    }
}
