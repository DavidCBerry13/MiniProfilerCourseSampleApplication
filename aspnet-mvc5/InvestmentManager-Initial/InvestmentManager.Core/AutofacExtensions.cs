using Autofac;
using InvestmentManager.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentManager.Core
{
    public static class AutofacExtensions
    {


        public static void RegisterServiceClasses(this ContainerBuilder builder, String stockIndexUrl)
        {
            builder.RegisterType<RateOfReturnService>().As<RateOfReturnService>().InstancePerRequest();
            builder.RegisterType<StockIndexService>().As<StockIndexService>()
                .WithParameter("baseUrl", stockIndexUrl)
                .SingleInstance();
        }

    }
}
