using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockIndexWebService.Data;
using StockIndexWebService.Domain;
using StockIndexWebService.ViewModel;

namespace StockIndexWebService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StockIndexPricesController : ControllerBase
    {

        public StockIndexPricesController(StockIndexDbContext dbContext)
        {
            this.dataContext = dbContext;
        }

        private StockIndexDbContext dataContext;


        // GET api/values
        [HttpGet("{indexCode}")]
        public ActionResult<StockIndexPriceModel> Get(String indexCode, DateTime tradeDate)
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
