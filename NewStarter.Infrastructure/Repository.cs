using Microsoft.EntityFrameworkCore;
using NewStarter.Domain.Model;
using NewStarterProject.NewStarter.Infrastructure.Datastore;
using System.Linq.Expressions;

namespace NewStarter.Infrastructure
{
    public class GenericRepository<TEntity> : IDataStore<TEntity> where TEntity : class
    {
        private readonly StarterProjectContext _dbContext;

        public GenericRepository(StarterProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbContext.Set<TEntity>().AsNoTracking();

            // Include related entities
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public async Task<TEntity?> GetById(int id)
        {
            return await _dbContext.Set<TEntity>()
                .FindAsync(id);
        }

        public async Task Create(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
