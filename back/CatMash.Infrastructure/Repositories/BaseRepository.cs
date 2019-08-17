using CatMash.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CatMash.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private CatMashContext _ctx;

        public BaseRepository(CatMashContext context)
        {
            _ctx = context;
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return _ctx.Set<T>().ToList();
        }

        public virtual async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var result = _ctx.Set<T>().AsQueryable();

            foreach (var includeExpression in includes)
            {
                result = result.Include(includeExpression);
            }

            return await result.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> searchBy, params Expression<Func<T, object>>[] includes)
        {
            var result = _ctx.Set<T>().Where(searchBy);

            foreach (var includeExpression in includes)
            {
                result = result.Include(includeExpression);
            }

            return await result.ToListAsync();
        }

        public virtual async Task<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var result = _ctx.Set<T>().Where(predicate);

            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);

            return await result.FirstOrDefaultAsync();
        }

        public virtual async Task Insert(T entity)
        {
            _ctx.Set<T>().Add(entity);
        }

        public virtual async Task Update(T entity)
        {
            _ctx.Set<T>().Attach(entity);
            _ctx.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task Delete(Expression<Func<T, bool>> identity, params Expression<Func<T, object>>[] includes)
        {
            var results = _ctx.Set<T>().Where(identity);

            foreach (var includeExpression in includes)
            {
                results = results.Include(includeExpression);
            }
            _ctx.Set<T>().RemoveRange(results);
        }

        public virtual async Task Delete(T entity)
        {
            _ctx.Set<T>().Remove(entity);
        }
    }
}
