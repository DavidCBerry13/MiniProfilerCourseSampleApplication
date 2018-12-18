using InvestmentManager.Core.Common;
using InvestmentManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentManager.Core.Services
{
    public class StockIndexService
    {

        public StockIndexService(IHttpClientFactory httpClientFactory)
        {           
            this.httpClientFactory = httpClientFactory;
        }

        private IHttpClientFactory httpClientFactory;



        public async Task<StockIndexInfo> GetStockIndexPrice(String indexCode, TradeDate date)
        {
            String url = $"api/StockIndexPrices/{indexCode}?tradeDate={date.Date.ToString("yyyy-MM-dd")}";
            var httpClient = httpClientFactory.CreateClient("StockIndexApi");

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<StockIndexInfo>();
            }
            else
            {
                return await Task.FromResult<StockIndexInfo>(null);
            }
        }


    }
}
