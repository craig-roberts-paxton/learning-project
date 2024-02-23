
using NewStarter.Domain.Dtos;
using System.Linq.Expressions;

namespace NewStarter.Domain.Model
{
    public interface IDataStore<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity?> GetById(params long[] id);

        Task Create(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(int id);
    }
}
