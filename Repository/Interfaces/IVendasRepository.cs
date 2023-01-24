using System.Collections.Generic;
using System.Threading.Tasks;
using api_vendas.Models;

namespace api_vendas.Repository.Interfaces
{
    public interface IVendasRepository : IBaseRepository
    {
        Task<IEnumerable<Venda>> GetVenda();
        Task<Venda> GetVendaById(int id);
    }
}