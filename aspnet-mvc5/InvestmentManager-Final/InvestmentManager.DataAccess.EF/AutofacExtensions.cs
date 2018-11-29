using Autofac;
using InvestmentManager.Core.DataAccess;
using InvestmentManager.DataAccess.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentManager.DataAccess.EF
{
    public static class AutofacExtensions
    {


        public static void RegisterEFDataAccessClasses(this ContainerBuilder builder, String connectionString)
        {
            builder.RegisterType<InvestmentContext>()
                .As<InvestmentContext>()
                .WithParameter("connectionString", connectionString)
                .InstancePerRequest();

            builder.RegisterType<SecurityRepository>().As<ISecurityRepository>().InstancePerRequest();
            builder.RegisterType<TradeDateRepository>().As<ITradeDateRepository>().InstancePerRequest();
            builder.RegisterType<InvestmentAccountRepository>().As<IInvestmentAccountRepository>().InstancePerRequest();
            builder.RegisterType<AccountPositionsRepository>().As<IAccountPositionsRepository>().InstancePerRequest();
            builder.RegisterType<AccountMarketValueRepository>().As<IAccountMarketValueRepository>().InstancePerRequest();
            builder.RegisterType<AccountCashFlowsRepository>().As<IAccountCashFlowsRepository>().InstancePerRequest();
            builder.RegisterType<SecurityPriceRepository>().As<ISecurityPriceRepository>().InstancePerRequest();
        }

    }
}
