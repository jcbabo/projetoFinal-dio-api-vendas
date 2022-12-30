using teste_tecnico_api_pagamentos.Repository.Interfaces;
using teste_tecnico_api_pagamentos.Context;
using System.Threading.Tasks;

namespace teste_tecnico_api_pagamentos.Repository
{
    public class BaseRepository : IBaseRepository
    {
        private readonly VendaContext _context;
        public BaseRepository(VendaContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
    }
}