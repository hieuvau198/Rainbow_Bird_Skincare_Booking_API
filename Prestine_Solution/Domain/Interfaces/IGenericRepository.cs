using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAllAsQueryable();
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<T?> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    }
}
