using StockIndexWebService.Data;
using StockIndexWebService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Data.Entity;
using System.Configuration;

namespace StockIndexWebService.Controllers
{
    public class StockIndexPricesController : ApiController
    {
        public StockIndexPricesController()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["StockIndexDatabase"].ConnectionString;
            this.dataContext = new StockIndexDbContext(connectionString);
        }

        private StockIndexDbContext dataContext;


        // GET api/values
        [Route("api/StockIndexPrices/{indexCode}")]
        public IHttpActionResult Get(String indexCode, DateTime tradeDate)
        {
            // Introduce some delay to simulate the delay you have in calling a service over the wire
            Thread.Sleep(new Random().Next(250, 750));

            var stockIndexPrice = this.dataContext.StockIndexPrices
                .Include(prop => prop.Index)
                .Where(p => p.IndexCode == indexCode && p.TradeDate == tradeDate)
                .FirstOrDefault();

            if (stockIndexPrice == null)
                return NotFound();

            var model = new StockIndexPriceModel()
            {
                IndexCode = stockIndexPrice.IndexCode,
                IndexName = stockIndexPrice.Index.Name,
                IndexShortDisplayName = stockIndexPrice.Index.ShortDisplayName,
                TradeDate = stockIndexPrice.TradeDate,
                ClosePrice = stockIndexPrice.ClosePrice,
                OpenPrice = stockIndexPrice.OpenPrice,
                HighPrice = stockIndexPrice.HighPrice,
                LowPrice = stockIndexPrice.LowPrice,
                AdjustedClosePrice = stockIndexPrice.AdjustedClosePrice,
                Volume = stockIndexPrice.Volume,
                Change = stockIndexPrice.Change,
                ChangePercent = stockIndexPrice.ChangePercent
            };

            return Ok(model);
        }
    }
}