using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestmentManager.Auth;
using InvestmentManager.Core;
using InvestmentManager.DataAccess.EF;
//using InvestmentManager.DataAccess.AdoNet;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using StackExchange.Profiling;
using StackExchange.Profiling.Storage;

namespace InvestmentManager
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            this.loggerFactory = loggerFactory;

            // For NLog                   
            env.ConfigureNLog( "nlog.config");
        }

        public IConfiguration Configuration { get; }

        private readonly ILoggerFactory loggerFactory;


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IConfiguration>(this.Configuration);

            // For Entity Framework and our data access
            var connectionString = this.Configuration.GetConnectionString("InvestmentDatabase");
            services.RegisterEfDataAccessClasses(connectionString, loggerFactory);

            // For Application Services
            String stockIndexServiceUrl = this.Configuration["StockIndexServiceUrl"];
            services.ConfigureStockIndexServiceHttpClientWithProfiler(stockIndexServiceUrl);
            services.ConfigureInvestmentManagerServices();

            // Auth
            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("UserDatabase")));
            services
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.SlidingExpiration = true;
            });


            // Miniprofiler
            var miniprofilerConectionString = this.Configuration.GetConnectionString("MiniProfilerDatabase");
            services.AddMiniProfiler(options =>
            {                
                options.PopupRenderPosition = StackExchange.Profiling.RenderPosition.BottomLeft;
                options.PopupShowTimeWithChildren = true;
                options.ResultsAuthorize = (request) => request.HttpContext.User.IsInRole("DevTeam");
                options.UserIdProvider = (request) => request.HttpContext.User.Identity.Name;

                options.Storage = new SqlServerStorage(miniprofilerConectionString);
            })
            .AddEntityFramework();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMiniProfiler();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
