using InvestmentManager.Core.DataAccess;
using InvestmentManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace InvestmentManager.ApiControllers
{

    [RoutePrefix("api/Securities")]
    public class SecuritiesController : ApiController
    {

        public SecuritiesController(ITradeDateRepository tradeDateRepository, ISecurityPriceRepository pricesRepository)
        {
            this.tradeDateRepository = tradeDateRepository;
            this.pricesRepository = pricesRepository;
        }

        private ITradeDateRepository tradeDateRepository;
        private ISecurityPriceRepository pricesRepository;


        // GET: api/AccountPositions/5
        [HttpGet]
        public IHttpActionResult Get(DateTime? date)
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
