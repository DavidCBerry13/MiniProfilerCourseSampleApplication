using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestmentManager.Core.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentManager.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountHistoryController : ControllerBase
    {


        public AccountHistoryController(ITradeDateRepository tradeDateRepository, IAccountMarketValueRepository marketValueRepository)
        {
            this.tradeDateRepository = tradeDateRepository;
            this.marketValueRepository = marketValueRepository;
        }

        private ITradeDateRepository tradeDateRepository;
        private IAccountMarketValueRepository marketValueRepository;


        [HttpGet("{accountNumber}", Name = "GetAccountHistory")]
        public IActionResult Get(String accountNumber)
        {
            var tradeDates = this.tradeDateRepository.LoadTradeDates();
            var monthEnds = tradeDates.Where(d => d.IsMonthEnd)
                .OrderBy(d => d.Date)
                .Select(d => d.Date)
                .ToList();


            var marketValues = this.marketValueRepository.LoadAccountMarketValues(accountNumber)
                .OrderBy(mv => mv.Date)
                .ToList();

            var monthEndMarketValues = marketValues
                .Where(mv => monthEnds.Any(monthEnd => monthEnd == mv.Date))
                .Select(mv => new { date = mv.Date, marketValue = mv.MarketValue })
                .ToList();

            if (monthEndMarketValues.Count != 0 && marketValues.Count != 0)
            {
                var firstMarketValue = marketValues.First();
                if (monthEndMarketValues.First().date != firstMarketValue.Date)
                    monthEndMarketValues.Insert(0, 
                        new { date = firstMarketValue.Date, marketValue = firstMarketValue.MarketValue });

                var lastMarketValue = marketValues.Last();
                if (monthEndMarketValues.Last().date != lastMarketValue.Date)
                    monthEndMarketValues.Add(
                        new { date = lastMarketValue.Date, marketValue = lastMarketValue.MarketValue });
            }

            return Ok(monthEndMarketValues);
        }



    }
}