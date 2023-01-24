using api_vendas.Repository.Interfaces;
using api_vendas.Context;
using System.Threading.Tasks;

namespace api_vendas.Repository
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