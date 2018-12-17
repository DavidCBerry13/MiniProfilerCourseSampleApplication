using System;
using Microsoft.Extensions.DependencyInjection;
using InvestmentManager.Core.DataAccess;
using InvestmentManager.DataAccess.Dapper.Repositories;



namespace InvestmentManager.DataAccess.Dapper
{
    public static class IServiceCollectionExtensions
    {


        public static void RegisterDapperDataAccessClasses(this IServiceCollection services,
            String connectionString)
        {
            services.AddScoped<ISecurityRepository>(serviceProvider => new DapperSecurityRepository(connectionString));
            services.AddScoped<ITradeDateRepository>(serviceProvider => new DapperTradeDateRepository(connectionString));
            services.AddScoped<IInvestmentAccountRepository>(serviceProvider => new DapperInvestmentAccountRepository(connectionString));
            services.AddScoped<IAccountPositionsRepository>(serviceProvider => new DapperAccountPositionsRepository(connectionString));
            services.AddScoped<IAccountMarketValueRepository>(serviceProvider => new DapperAccountMarketValueRepository(connectionString));
            services.AddScoped<IAccountCashFlowsRepository>(serviceProvider => new DapperAccountCashFlowsRepository(connectionString));
            services.AddScoped<ISecurityPriceRepository>(serviceProvider => new DapperSecurityPriceRepository(connectionString));
        }

    }
}
