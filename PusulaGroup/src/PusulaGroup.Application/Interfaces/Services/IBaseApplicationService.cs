using PusulaGroup.Application.Interfaces.Repositories;
using PusulaGroup.Domain.Entities;
using System.Linq.Expressions;

namespace PusulaGroup.Application.Interfaces.Services
{
    public interface IBaseApplicationService<TEntity>
    where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetListAsync();
        Task<List<TEntity>> GetListByPredicateAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetByPredicateAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteByPredicateAsync(Expression<Func<TEntity, bool>> predicate);
        
    }
}
