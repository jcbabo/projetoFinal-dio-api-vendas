using System.Collections.Generic;
using System.Threading.Tasks;
using teste_tecnico_api_pagamentos.Models;

namespace teste_tecnico_api_pagamentos.Repository.Interfaces
{
    public interface IVendasRepository : IBaseRepository
    {
        Task<IEnumerable<Venda>> GetVenda();
        Task<Venda> GetVendaById(int id);
    }
}