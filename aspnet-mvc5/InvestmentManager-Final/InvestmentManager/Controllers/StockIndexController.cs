using InvestmentManager.Core.DataAccess;
using InvestmentManager.Core.Domain;
using InvestmentManager.Core.Services;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InvestmentManager.Controllers
{
    public class StockIndexController : Controller
    {

        public StockIndexController(ITradeDateRepository tradeDateRepository, StockIndexService service)
        {
            this.tradeDateRepository = tradeDateRepository;
            this.stockIndexService = service;
        }

        private ITradeDateRepository tradeDateRepository;
        private StockIndexService stockIndexService;


        // GET: StockIndex/Details/5
        [Route("StockIndex/Details/{indexCode}")]
        [ChildActionOnly]
        public ActionResult Details(String indexCode)
        {
            var currentTradeDate = this.tradeDateRepository.GetLatestTradeDate();

            // Grab a copy of the current profiler and pass it in to the GetStockIndexPrice() method
            // because this method runs in a separate task and can't access the static property MiniProfiler.Current
            // from within that task
            var profiler = MiniProfiler.Current;
            StockIndexInfo indexInfo = AsyncHelper.RunSync<StockIndexInfo>(
                () => stockIndexService.GetStockIndexPrice(indexCode, currentTradeDate, profiler));
         
            return PartialView(indexInfo);
        }

    }



    public static class AsyncHelper
    {
        private static readonly TaskFactory _taskFactory = new
            TaskFactory(CancellationToken.None,
                        TaskCreationOptions.None,
                        TaskContinuationOptions.None,
                        TaskScheduler.Default);

        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
            => _taskFactory
                .StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();

        public static void RunSync(Func<Task> func)
            => _taskFactory
                .StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
    }


}
