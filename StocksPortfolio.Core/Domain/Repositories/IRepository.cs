using MongoDB.Bson;
using StocksPortfolio.Core.Domain.Entities;

namespace StocksPortfolio.Core.Domain.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : Entity
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<IQueryable<TEntity>> QueryAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetAsync(ObjectId id);
    }
}
