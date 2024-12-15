using Microsoft.EntityFrameworkCore;
using TimeLog.Service.Repository.Interface;

namespace TimeLog.Service.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public IQueryable<T> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<List<T>> GetPaginableListAsync(int currentPage, int pageSize)
        {
            var resultList = await _dbSet.ToListAsync();
            var totalCount = resultList.Count;
            int TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            int CurrentPage = currentPage;
            List<T> Items = resultList.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return Items;
        }
    }
}
