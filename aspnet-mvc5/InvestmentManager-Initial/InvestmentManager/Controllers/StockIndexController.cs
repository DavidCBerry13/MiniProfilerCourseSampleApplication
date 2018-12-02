using InvestmentManager.Core.DataAccess;
using InvestmentManager.Core.Domain;
using InvestmentManager.Core.Services;
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

            StockIndexInfo indexInfo = AsyncHelper.RunSync<StockIndexInfo>(
                () => stockIndexService.GetStockIndexPrice(indexCode, currentTradeDate));
         
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
