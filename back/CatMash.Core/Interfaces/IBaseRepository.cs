using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CatMash.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> GetAll(params string[] includes);

        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> searchBy, params string[] includes);

        Task<T> FindBy(Expression<Func<T, bool>> predicate, params string[] includes);

        Task Insert(T entity);

        Task Update(T entity);

        Task Delete(Expression<Func<T, bool>> identity, params Expression<Func<T, object>>[] includes);

        Task Delete(T entity);

    }
}
