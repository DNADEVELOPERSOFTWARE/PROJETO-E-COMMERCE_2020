using Application.Interfaces.IProduto;
using Domain.Interfaces.InterfaceProduto;
using Domain.Interfaces.InterfaceServico;
using Entity.Entities.Produtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.OpenApp.AppProdutos
{
    public class AppProduto : InterfaceProdutoApp
    {
        IProduto _IProduto;
        IServiceProduto _IServiceProduto;
        public AppProduto(IProduto IProduto, IServiceProduto IServiceProduto)
        {
            _IProduto = IProduto;
            _IServiceProduto = IServiceProduto;
        }

        //Métodos do CRUD
        public async Task Add(Produto Objeto)
        {
            await _IProduto.Add(Objeto);
        }
        public async Task Delete(Produto Objeto)
        {
            await _IProduto.Delete(Objeto);
        }
        public async Task<Produto> GetEntityById(int Id)
        {
            return await _IProduto.GetEntityById(Id);
        }
        public async Task<List<Produto>> List()
        {
            return await _IProduto.List();
        }
        public async Task Update(Produto Objeto)
        {
            await _IProduto.Update(Objeto);
        }

        #region MÉTODOS CUSTUMIZADOS

        //Métodos personalizados com regra de negócio
        public async Task AddProduto(Produto produto)
        {
            await _IServiceProduto.AddProduto(produto);
        }
        public async Task UpdateProduto(Produto produto)
        {
            await _IServiceProduto.UpdateProduto(produto);
        }

        public async Task<List<Produto>> ListarProdutoUsuario(string userId)
        {
            return await _IProduto.ListarProdutoUsuario(userId);
        }
        public async Task<List<Produto>> ListaProdutoComEstoque(string descricao)
        {
            return await _IServiceProduto.ListarProdutosComEstoque(descricao);
        }
        public async Task<List<Produto>> ListarProdutoCarrinhoUsuario(string userId)
        {

            return await _IProduto.ListarProdutoCarrinhoUsuario(userId);    
        }
        public async Task<Produto> ObterProdutoCarrinho(int ProdutoCarrinhoId)
        {

            return await _IProduto.ObterProdutoCarrinho(ProdutoCarrinhoId);
        }
        #endregion
    }
}
