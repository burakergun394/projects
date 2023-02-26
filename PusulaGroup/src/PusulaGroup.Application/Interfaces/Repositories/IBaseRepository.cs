using PusulaGroup.Domain.Entities;
using System.Linq.Expressions;

namespace PusulaGroup.Application.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    where T : BaseEntity
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetListAsync();
        Task<List<T>> GetListByPredicateAsync(Expression<Func<T, bool>> predicate);
    }
}
