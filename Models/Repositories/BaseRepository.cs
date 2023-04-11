using BackEndTestTask.Models.Database.Context;
using BackEndTestTask.Models.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BackEndTestTask.Models.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDatabaseContext _context;

        public BaseRepository(AppDatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException($"{nameof(entity)} is null");
            }
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException($"{nameof(entity)} is null");
            }
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException($"{nameof(entity)} is null");
            }
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>>? filter)
        { 
            return await _context.Set<T>().FirstOrDefaultAsync(filter);
        } 

        public async Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>>? filter)
        {
            return await _context.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetPagedAsync(int page, int pageSize)
        {
            return await _context.Set<T>()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>>? filter)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(filter);
        }
    }
}