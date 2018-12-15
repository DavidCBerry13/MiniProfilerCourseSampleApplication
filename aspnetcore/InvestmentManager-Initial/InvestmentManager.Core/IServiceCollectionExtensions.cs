using InvestmentManager.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvestmentManager.Core
{
    public static class IServiceCollectionExtensions
    {


        public static void ConfigureInvestmentManagerServices(this IServiceCollection services, 
            String stockIndexUrl)
        {
            services.AddScoped<RateOfReturnService, RateOfReturnService>();
            services.AddSingleton<StockIndexService, StockIndexService>();
        }


        public static void ConfigureStockIndexServiceHttpClientWithoutProfiler(
            this IServiceCollection services, String serviceUrl)
        {
            services.AddHttpClient("StockIndexApi", c =>
            {
                c.BaseAddress = new Uri(serviceUrl);
            });
        }

    }
}
