using eShopEnterprise.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopEnterprise.Catalogo.API.Models
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<PagedResult<Produto>> ObterTodos(int pageSize, int pageIndex, string query = null);
        Task<IEnumerable<Produto>> ObterTodos();
        Task<Produto> ObterPorId(Guid id);
        Task<List<Produto>> ObterProdutosPorId(string ids);
        Task Adicionar(Produto produto);
        void Atualizar(Produto produto);
    }
}
