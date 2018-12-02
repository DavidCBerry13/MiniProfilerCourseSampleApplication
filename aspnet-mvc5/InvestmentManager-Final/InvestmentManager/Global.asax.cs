using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;
using StackExchange.Profiling.Mvc;
using StackExchange.Profiling.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace InvestmentManager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutofacConfig.Configure();

            var miniprofilerDb = "Server=localhost\\sql2016;Database=Miniprofiler;Trusted_Connection=True;Application Name=Investment Manager MiniProfiler";

            MiniProfiler.Configure(new MiniProfilerOptions()
            {
                //Storage = new SqlServerStorage(miniprofilerDb),

                // Sets up the route to use for MiniProfiler resources:
                RouteBasePath = "~/miniprofiler",
                PopupRenderPosition = RenderPosition.BottomLeft
            })
            .AddViewProfiling();
            MiniProfilerEF6.Initialize();
        }


        protected void Application_BeginRequest()
        {
            MiniProfiler.StartNew();   
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Current?.Stop();
        }
    }
}
