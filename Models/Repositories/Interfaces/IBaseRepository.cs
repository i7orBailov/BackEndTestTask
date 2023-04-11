using System.Linq.Expressions;

namespace BackEndTestTask.Models.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetFirstAsync(Expression<Func<T, bool>>? filter);
        Task<T> GetSingleAsync(Expression<Func<T, bool>>? filter);
        Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>>? filter);
        Task<IEnumerable<T>> GetPagedAsync(int page, int pageSize);
        Task SaveChangesAsync();
    }
}