namespace TimeLog.Service.Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        IQueryable<T> AsQueryable();
        Task<List<T>> GetPaginableListAsync(int currentPage, int pageSize);
    }
}
