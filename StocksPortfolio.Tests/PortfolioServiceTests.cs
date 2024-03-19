using Moq;
using AutoMapper;
using MongoDB.Bson;
using StocksPortfolio.Core.Contracts;
using StocksPortfolio.Core.Services;
using StocksPortfolio.Core.Domain.Entities;
using StocksPortfolio.Infrastructure.Persistence.Repositories;
using StocksPortfolio.Infrastructure.Persistence.Configuration.Abstract;

namespace StocksPortfolio.Tests
{
    [TestClass]
    public class PortfolioServiceTests
    {
        [TestMethod]
        public async Task Soft_delete_portfolio()
        {
            //ARRANGE
            var mongoDbConfig = new Mock<IMongoDbConfiguration>();
            mongoDbConfig.Setup(x => x.CollectionName).Returns("Portfolios");
            mongoDbConfig.Setup(x => x.DatabaseName).Returns("portfolioDb");
            mongoDbConfig.Setup(x => x.FileName).Returns("portfoliosTest.json");
            MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Portfolio, PortfolioModel>());
            var mapper = config.CreateMapper();
            var runner = new AppMongoDbRunner(mongoDbConfig.Object);
            var repository = new Repository<Portfolio>(mongoDbConfig.Object, runner);
            var service = new PortfolioService(repository, mapper);

            //ACT
            var id = new ObjectId("50227b375dff9218248eadc4");
            await service.SoftDeleteAsync(id);
            var softDeletedPortfolio = (await repository.QueryAsync(x => x.Id == id && x.IsDeleted == true)).Single();
            runner.Dispose();

            //ASSERT
            Assert.IsTrue(softDeletedPortfolio.IsDeleted);
        }
    }
}
