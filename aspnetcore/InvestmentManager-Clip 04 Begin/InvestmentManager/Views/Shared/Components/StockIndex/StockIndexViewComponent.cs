using InvestmentManager.Core.DataAccess;
using InvestmentManager.Core.Domain;
using InvestmentManager.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestmentManager.Views.Shared.Components.StockIndex
{
    public class StockIndexViewComponent : ViewComponent
    {

        public StockIndexViewComponent(StockIndexService service, ITradeDateRepository tradeDateRepository)
        {
            this.stockIndexService = service;
            this.tradeDateRepository = tradeDateRepository;
        }

        private StockIndexService stockIndexService;
        private ITradeDateRepository tradeDateRepository;

        


        public async Task<IViewComponentResult> InvokeAsync(string indexCode)
        {
            var currentTradeDate = this.tradeDateRepository.GetLatestTradeDate();
            //Task<StockIndexInfo> indexTask = stockIndexService.GetStockIndexPrice(indexCode, currentTradeDate);
            //indexTask.Wait();

            //StockIndexInfo indexInfo = indexTask.Result;
            StockIndexInfo indexInfo = await stockIndexService.GetStockIndexPrice(indexCode, currentTradeDate);


            return View("Default", indexInfo);
        }

    }
}
