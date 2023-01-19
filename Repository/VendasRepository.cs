using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teste_tecnico_api_pagamentos.Repository.Interfaces;
using teste_tecnico_api_pagamentos.Context;
using teste_tecnico_api_pagamentos.Models;
using Microsoft.EntityFrameworkCore;

namespace teste_tecnico_api_pagamentos.Repository
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