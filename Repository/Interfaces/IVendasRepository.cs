using System.Collections.Generic;
using System.Threading.Tasks;
using teste_tecnico_api_pagamentos.Models;

namespace teste_tecnico_api_pagamentos.Repository.Interfaces
{
    public interface IVendasRepository : IBaseRepository
    {
        Task<IEnumerable<Vendedor>> GetVendedor();
        Task<Vendedor> GetVendedorById(int id);
        Task<IEnumerable<Venda>> GetVenda();
        Task<Venda> GetVendaById(int id);
        Task<IEnumerable<Produto>> GetProduto();
        Task<Produto> GetProdutoById(int id);
    }
}