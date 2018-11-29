using InvestmentManager.Core.Common;
using InvestmentManager.Core.Domain;
using Microsoft.Extensions.Configuration;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentManager.Core.Services
{
    public class StockIndexService
    {

        public StockIndexService(String baseUrl)
        {
            var clientHandler = new HttpClientHandler();
            var profilingHandler = new ProfilingHttpHandler(clientHandler);            
            this.httpClient = new HttpClient(profilingHandler);
            this.httpClient.BaseAddress = new Uri(baseUrl);
        }


        private String baseUrl;
        private HttpClient httpClient;



        public async Task<StockIndexInfo> GetStockIndexPrice(String indexCode, TradeDate date)
        {
            StockIndexInfo indexInfo = null;

            String url = $"api/StockIndexPrices/{indexCode}?tradeDate={date.Date.ToString("yyyy-MM-dd")}";
            using (var timing = MiniProfiler.Current.Step($"Get Stock Index Info: {indexCode}"))
            {
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    indexInfo = await response.Content.ReadAsAsync<StockIndexInfo>();
                }
                
                return indexInfo;
            }
        }


    }
}
