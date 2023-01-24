using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_vendas.Repository.Interfaces;
using api_vendas.Context;
using api_vendas.Models;
using Microsoft.EntityFrameworkCore;

namespace api_vendas.Repository
{
    public class VendasRepository : BaseRepository, IVendasRepository
    {
        private readonly VendaContext _context;

        public VendasRepository(VendaContext context) : base(context)
        {
            _context = context;
        }

        // Vendas
        public async Task<IEnumerable<Venda>> GetVenda()
        {
            var venda = await _context.InfoVendas!
            
            .Include(x => x.Itens)
            .Include(x => x.VendedorId)
            .ToListAsync();

            return venda;
        }

        public async Task<Venda> GetVendaById(int id)
        {
            var venda = await _context.InfoVendas!
            
            .Include(x => x.Itens)
            .Where(x => x.Id == id).FirstOrDefaultAsync();
            
            return venda!;
        }
    }
}