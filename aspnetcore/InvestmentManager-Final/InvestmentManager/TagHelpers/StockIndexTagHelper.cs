using InvestmentManager.Core.DataAccess;
using InvestmentManager.Core.Domain;
using InvestmentManager.Core.Services;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestmentManager.TagHelpers
{

    [HtmlTargetElement("stock-index")]
    public class StockIndexTagHelper : TagHelper
    {

        public StockIndexTagHelper(StockIndexService service, ITradeDateRepository tradeDateRepository)
        {
            this.stockIndexService = service;
            this.tradeDateRepository = tradeDateRepository;
        }

        private StockIndexService stockIndexService;
        private ITradeDateRepository tradeDateRepository;
        
        public string IndexCode { get; set; }

        

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var currentTradeDate = this.tradeDateRepository.GetLatestTradeDate();
            Task<StockIndexInfo> indexTask = stockIndexService.GetStockIndexPrice(IndexCode, currentTradeDate);
            indexTask.Wait();

            StockIndexInfo indexInfo = indexTask.Result;

            output.TagName = "div";    // Replaces <email> with <a> tag
            //<div class="col-md-3 small">DJIA: 28,384.27 +23.42 (+1.2%)</div>
            output.Attributes.SetAttribute("class", "col-md-3 small text-center");
            output.Content.SetContent($"<strong>{indexInfo.IndexShortDisplayName}:</strong> {indexInfo.AdjustedClosePrice:0,0.00}  {indexInfo.Change:0,0.00} ({indexInfo.ChangePercent:0.00%})");
        }


    }
}
