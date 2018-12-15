using InvestmentManager.Core.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using InvestmentManager.DataAccess.AdoNet.Repositories;

namespace InvestmentManager.DataAccess.AdoNet
{
    public static class IServiceCollectionExtensions
    {

        public static void RegisterAdoCommonDataAccessClasses(this IServiceCollection services,
            String connectionString)
        {
            services.AddScoped<ISecurityRepository>(serviceProvider => new AdoSecurityRepository(connectionString));
            services.AddScoped<ITradeDateRepository>(serviceProvider => new AdoTradeDateRepository(connectionString));
            services.AddScoped<IInvestmentAccountRepository>(serviceProvider => new AdoInvestmentAccountRepository(connectionString));
            services.AddScoped<IAccountPositionsRepository>(serviceProvider => new AdoAccountPositionsRepository(connectionString));
            services.AddScoped<IAccountMarketValueRepository>(serviceProvider => new AdoAccountMarketValueRepository(connectionString));
            services.AddScoped<IAccountCashFlowsRepository>(serviceProvider => new AdoAccountCashFlowsRepository(connectionString));
            services.AddScoped<ISecurityPriceRepository>(serviceProvider => new AdoSecurityPriceRepository(connectionString));
        }

    }
}
