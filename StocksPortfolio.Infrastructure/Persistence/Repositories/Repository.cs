using Mongo2Go;
using MongoDB.Bson;
using MongoDB.Driver;
using StocksPortfolio.Core.Domain.Entities;
using StocksPortfolio.Core.Domain.Repositories;
using StocksPortfolio.Infrastructure.Persistence.Configuration.Abstract;
using StocksPortfolio.Infrastructure.Persistence.Repositories.Abstract;

namespace StocksPortfolio.Infrastructure.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        private readonly IAppMongoDbRunner _runner;
        private readonly IMongoDatabase _database;
        private static IMongoDbConfiguration _config;
        private readonly IMongoCollection<TEntity> _collection;

        public Repository(IMongoDbConfiguration config, IAppMongoDbRunner runner)
        {
            _runner = runner;
            _config = config;
            _database = GetDatabase();
            _collection = GetCollection();
        }

        public Task AddAsync(TEntity entity)
        {
            entity.Id = new ObjectId();
            return _collection.InsertOneAsync(entity);
        }

        public Task UpdateAsync(TEntity entity)
        {
            if (!entity.Id.HasValue)
            {
                throw new ArgumentException($"Provided entity {typeof(TEntity).Name} is missing id value", nameof(entity));
            }

            return _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

        public Task DeleteAsync(TEntity entity)
        {
            if (!entity.Id.HasValue)
            {
                throw new ArgumentException($"Provided entity {typeof(TEntity).Name} is missing id value", nameof(entity));
            }

            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);
            return _collection.DeleteOneAsync(filter);
        }

        public Task<IQueryable<TEntity>> QueryAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            var result = _collection.AsQueryable().Where(predicate);
            return Task.FromResult(result);
        }

        public Task<TEntity> GetAsync(ObjectId id)
        {
            var result = _collection.AsQueryable().Where(x => x.Id == id).FirstOrDefault();
            return Task.FromResult(result);
        }

        private IMongoDatabase GetDatabase()
        {
            var client = new MongoClient(_runner.ConnectionString);
            return client.GetDatabase(_config.DatabaseName);
        }

        private IMongoCollection<TEntity> GetCollection()
        {
            return _database.GetCollection<TEntity>($"{typeof(TEntity).Name}s");
        }
    }
}
