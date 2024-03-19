using StocksPortfolio.Core.Contracts;
using StocksPortfolio.Core.Domain.Entities;
using StocksPortfolio.Core.Domain.Repositories;
using StocksPortfolio.Core.Services;
using StocksPortfolio.Core.Services.Abstract;
using StocksPortfolio.Infrastructure.Persistence.WebServices;
using StocksPortfolio.Infrastructure.Persistence.Configuration;
using StocksPortfolio.Infrastructure.Persistence.Configuration.Abstract;
using StocksPortfolio.Infrastructure.Persistence.Repositories;
using StocksPortfolio.Infrastructure.Persistence.WebServices.Abstract;
using System;
using Mongo2Go.Helper;
using StocksPortfolio.Infrastructure.Persistence.Repositories.Abstract;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<Portfolio, PortfolioModel>();
    cfg.CreateMap<Stock, StockModel>();
});

builder.Services.AddTransient<IStocksService, StocksService>();
builder.Services.AddTransient<ICurrencyRateService, CurrencyLayerApiService>();
builder.Services.AddTransient<IMongoDbConfiguration, AppConfiguration>();
builder.Services.AddTransient<IRepository<Portfolio>, Repository<Portfolio>>();
builder.Services.AddTransient<IEntityService<PortfolioModel>, PortfolioService>();
builder.Services.AddTransient<ICurrencyLayerApiConfiguration, AppConfiguration>();
builder.Services.AddSingleton<IAppMongoDbRunner, AppMongoDbRunner>();

builder.Services.AddHttpClient();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
