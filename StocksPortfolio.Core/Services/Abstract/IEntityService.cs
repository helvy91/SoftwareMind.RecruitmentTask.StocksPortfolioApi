using MongoDB.Bson;

namespace StocksPortfolio.Core.Services.Abstract
{
    public interface IEntityService<TModel>
    {
        Task CreateAsync(TModel model);
        Task DeleteAsync(ObjectId id);
        Task<TModel> GetAsync(ObjectId id);
        Task UpdateAsync(TModel model);
        Task SoftDeleteAsync(ObjectId id);
    }
}