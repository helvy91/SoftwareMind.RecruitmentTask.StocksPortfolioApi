using AutoMapper;
using MongoDB.Bson;
using StocksPortfolio.Core.Contracts;
using StocksPortfolio.Core.Domain.Entities;
using StocksPortfolio.Core.Services.Abstract;
using StocksPortfolio.Core.Domain.Repositories;

namespace StocksPortfolio.Core.Services
{
    public class PortfolioService : IEntityService<PortfolioModel>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Portfolio> _repository;

        public PortfolioService(IRepository<Portfolio> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<PortfolioModel> GetAsync(ObjectId id)
        {
            var entity = (await _repository.QueryAsync(x => x.Id == id && !x.IsDeleted)).FirstOrDefault();
            return _mapper.Map<PortfolioModel>(entity);
        }

        public async Task CreateAsync(PortfolioModel model)
        {
            var entity = _mapper.Map<Portfolio>(model);
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(PortfolioModel model)
        {
            var entity = _mapper.Map<Portfolio>(model);
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(ObjectId id)
        {
            var entity = await _repository.GetAsync(id);
            await _repository.DeleteAsync(entity);
        }

        public async Task SoftDeleteAsync(ObjectId id)
        {
            var portfolio = await _repository.GetAsync(id);
            portfolio.IsDeleted = true;
            await _repository.UpdateAsync(portfolio);
        }
    }
}
