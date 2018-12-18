//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using InvestmentManager.Core.DataAccess;
//using InvestmentManager.Core.Domain;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Common;

//namespace InvestmentManager.ApiControllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class RateOfReturnController : ControllerBase
//    {

//        public RateOfReturnController(ITradeDateRepository tradeDateRepository, 
//            IAccountMarketValueRepository marketValueRepository, IAccountCashFlowsRepository cashFlowRepository)
//        {
//            this.tradeDateRepository = tradeDateRepository;
//            this.marketValueRepository = marketValueRepository;
//            this.cashFlowRepository = cashFlowRepository;
//        }

//        private ITradeDateRepository tradeDateRepository;
//        private IAccountMarketValueRepository marketValueRepository;
//        private IAccountCashFlowsRepository cashFlowRepository;


//        [HttpGet("{accountNumber}", Name = "GetAccountRateOfReturn")]
//        public IActionResult Get(String accountNumber)
//        {
//            var tradeDates = this.tradeDateRepository.LoadTradeDates();
//            var monthEnds = tradeDates.Where(d => d.IsMonthEnd)
//                .OrderBy(d => d.Date)
//                .Select(d => d.Date)
//                .ToList();

//            var marketValues = this.marketValueRepository.LoadAccountMarketValues(accountNumber)
//                .OrderBy(mv => mv.Date)
//                .ToList();

//            var periodEndMarketValues = marketValues
//                .Where(mv => monthEnds.Any(monthEnd => monthEnd == mv.Date))
//                .ToList();

//            if (periodEndMarketValues.Count != 0 && marketValues.Count != 0)
//            {
//                var firstMarketValue = marketValues.First();
//                if (periodEndMarketValues.First().Date != firstMarketValue.Date)
//                    periodEndMarketValues.Insert(0, firstMarketValue);

//                var lastMarketValue = marketValues.Last();
//                if (periodEndMarketValues.Last().Date != lastMarketValue.Date)
//                    periodEndMarketValues.Add(lastMarketValue);
//            }

//            List<PeriodInfo> performancePeriods = new List<PeriodInfo>();
//            for (int i = 1; i < periodEndMarketValues.Count; i++)
//            {
//                var startingMarketValue = periodEndMarketValues[i - 1];
//                var endingMarketValue = periodEndMarketValues[i];

//                PeriodInfo period = new PeriodInfo()
//                {
//                    StartingDate = new DateTime(endingMarketValue.Date.Year, endingMarketValue.Date.Month, 1),
//                    StartingMarketValue = startingMarketValue.MarketValue,
//                    EndingDate = endingMarketValue.Date,
//                    EndingMarketValue = endingMarketValue.MarketValue
//                };
//                performancePeriods.Add(period);
//            }

//            performancePeriods[0].StartingMarketValue = 0.0m;   // Fix up the starting market value to be zero

//            var cashFlows = this.cashFlowRepository.LoadExternalCashFlows(accountNumber);
//            foreach (var cashFlow in cashFlows)
//            {
//                var period = performancePeriods
//                    .Where(pp => cashFlow.Date >= pp.StartingDate && cashFlow.Date <= pp.EndingDate)
//                    .First();

//                period.CashFlows.Add(cashFlow.Amount);
//            }

//            // inception to date

//            var inceptionToDateReturn = performancePeriods
//                .Select(pp => ( 1 + pp.MonthlyRateOfReturn))                
//                .Aggregate((sum, x) => sum * (x)) -1;

//            var currentYear = performancePeriods.Last().EndingDate.Year;
//            var yearToDateReturn = performancePeriods
//                .Where(pp => pp.EndingDate.Year == currentYear)
//                .Select(pp => (1 + pp.MonthlyRateOfReturn))
//                .Aggregate((sum, x) => sum * (x)) - 1;


//            var currentQuarter = performancePeriods.Last().EndingDate.Quarter();
//            var quarterToDateReturn = performancePeriods
//                .Where(pp => pp.EndingDate.Year == currentYear && pp.EndingDate.Quarter() == currentQuarter)
//                .Select(pp => (1 + pp.MonthlyRateOfReturn))
//                .Aggregate((sum, x) => sum * (x)) - 1;


//            return Ok(new { monthlyReturns = performancePeriods,
//                inceptionToDateReturn = inceptionToDateReturn,
//                yearToDateReturn = yearToDateReturn,
//                quarterToDateReturn = quarterToDateReturn,
//                monthToDateReturn = performancePeriods.Last().MonthlyRateOfReturn
//            });
//        }

//    }
//}