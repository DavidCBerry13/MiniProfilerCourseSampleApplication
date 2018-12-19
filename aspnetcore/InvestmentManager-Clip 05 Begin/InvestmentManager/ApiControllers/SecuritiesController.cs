using InvestmentManager.Core.DataAccess;
using InvestmentManager.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestmentManager.ApiControllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SecuritiesController : ControllerBase
    {

        public SecuritiesController(ITradeDateRepository tradeDateRepository, ISecurityPriceRepository pricesRepository)
        {
            this.tradeDateRepository = tradeDateRepository;
            this.pricesRepository = pricesRepository;
        }

        private ITradeDateRepository tradeDateRepository;
        private ISecurityPriceRepository pricesRepository;


        // GET: api/AccountPositions/5
        [HttpGet(Name = "GetPrices")]
        public IActionResult Get([FromQuery]DateTime? date)
        {
            TradeDate tradeDate = null;
            if (date.HasValue )
            {
                var tradeDates = this.tradeDateRepository.LoadTradeDates();
                tradeDate = tradeDates.FirstOrDefault(td => td.Date == date.Value);
                if (tradeDate == null)
                    return BadRequest($"The date {date:yyyy-mm-DD} is not a valid trade date");
            }
            else
            {
                tradeDate = this.tradeDateRepository.GetLatestTradeDate();
            }


            var prices = this.pricesRepository.LoadSecurityPrices(tradeDate);

            var results = prices.Select(p => new
            {
                symbol = p.Symbol,
                name = p.Security.Name,
                sector = p.Security.Sector,
                industry = p.Security.Industry,
                open = p.OpenPrice,
                close = p.ClosePrice,
                low = p.DailyLow,
                high = p.DailyHigh,
                change = p.Change,
                changePercent = p.ChangePercent,
                date = p.Date,
                volume = p.Volume,
                description = p.Security.Description
            });



            return Ok(results);
        }

    }
}
