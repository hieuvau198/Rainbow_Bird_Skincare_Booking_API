using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly PrestinedbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(PrestinedbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAllAsQueryable()
        {
            return _dbSet.AsNoTracking();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var keyProperty = _context.Model.FindEntityType(typeof(T))?
                .FindPrimaryKey()?.Properties.FirstOrDefault()?.Name;

            if (string.IsNullOrEmpty(keyProperty))
                throw new InvalidOperationException($"Primary key not found for entity {typeof(T).Name}");

            return includes.Length > 0
                ? await query.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, keyProperty) == id)
                : await _dbSet.FindAsync(id);
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().CountAsync(predicate);
        }
    }
}
