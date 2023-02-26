using Ardalis.GuardClauses;
using PusulaGroup.Application.Interfaces.Repositories;
using PusulaGroup.Application.Interfaces.Services;
using PusulaGroup.Application.Interfaces.UnitOfWork;
using PusulaGroup.Domain.Entities;
using System.Linq.Expressions;

namespace PusulaGroup.Application.Services
{
    public class BaseApplicationService<TEntity, TRepository> : IBaseApplicationService<TEntity>
     where TEntity : BaseEntity
     where TRepository : IBaseRepository<TEntity>
    {
        protected readonly TRepository Repository;
        protected readonly IUnitOfWork UnitOfWork;

        public BaseApplicationService(TRepository repository, IUnitOfWork unitOfWork)
        {
            Repository = repository;
            UnitOfWork = unitOfWork;
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            Guard.Against.Null(entity);

            var createdEntity = await Repository.CreateAsync(entity);
            await UnitOfWork.SaveChangesAsync();
            return createdEntity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            Guard.Against.Null(entity);

            var updatedEntity = await Repository.UpdateAsync(entity);
            await UnitOfWork.SaveChangesAsync();
            return updatedEntity;
        }

        public virtual async Task<List<TEntity>> GetListAsync()
        {
            var datas = await Repository.GetListAsync();
            return datas;
        }

        public virtual async Task<List<TEntity>> GetListByPredicateAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var datas = await Repository.GetListByPredicateAsync(predicate);
            return datas;
        }

        public virtual async Task<TEntity> GetByPredicateAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var data = await Repository.GetAsync(predicate);
            return data;
        }

        public virtual async Task DeleteByPredicateAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var deletingEntity = await GetByPredicateAsync(predicate);
            Guard.Against.Null(deletingEntity);

            await Repository.DeleteAsync(deletingEntity);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Repository.AnyAsync(predicate);
        }
    }
}
