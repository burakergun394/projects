using Microsoft.EntityFrameworkCore;
using PusulaGroup.Application.Interfaces.Repositories;
using PusulaGroup.Domain.Entities;
using System.Linq.Expressions;

namespace PusulaGroup.Infrastructure.EntityFrameworkCore.Repositories
{
    public class EfCoreBaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
    where TEntity : BaseEntity
    where TContext : DbContext
    {
        private readonly TContext context;

        public EfCoreBaseRepository(TContext context)
        {
            this.context = context;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
            return Task.FromResult(entity);
        }

        public Task DeleteAsync(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<List<TEntity>> GetListByPredicateAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<List<TEntity>> GetListAsync() { 
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().AnyAsync(predicate);
        }
    }
}
