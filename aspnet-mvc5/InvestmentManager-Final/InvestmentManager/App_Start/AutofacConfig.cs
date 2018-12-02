using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using InvestmentManager.Core;
using InvestmentManager.DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace InvestmentManager
{
    public static class AutofacConfig
    {

        public static void Configure()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            //builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Data Access
            var investmentDbConnectionString = ConfigurationManager.ConnectionStrings["InvestmentDatabase"].ConnectionString;
            builder.RegisterEFDataAccessClasses(investmentDbConnectionString);

            // Services
            var stockIndexUrl = ConfigurationManager.AppSettings["StockIndexUrl"];
            builder.RegisterServiceClasses(stockIndexUrl);

            // Set the dependency resolver to be Autofac for MVC
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // For WebAPI
            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}