using InvestmentManager.Core.DataAccess;
using InvestmentManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InvestmentManager.Core.Common;
using MoreLinq;
using System.Threading;

namespace InvestmentManager.Core.Services
{
    public class RateOfReturnService
    {

        public RateOfReturnService(ITradeDateRepository tradeDateRepository,
            IAccountMarketValueRepository marketValueRepository, IAccountCashFlowsRepository cashFlowRepository)
        {
            this.tradeDateRepository = tradeDateRepository;
            this.marketValueRepository = marketValueRepository;
            this.cashFlowRepository = cashFlowRepository;
        }


        private ITradeDateRepository tradeDateRepository;
        private IAccountMarketValueRepository marketValueRepository;
        private IAccountCashFlowsRepository cashFlowRepository;


        public List<PeriodSummary> CalculatePerformance(String accountNumber)
        {
            var marketValues = this.GetAccountMarketValues(accountNumber);
            var cashFlows = this.GetAccountCashFlows(accountNumber);

            // If we only have one market value, the account has only been open for a day
            // so don't bother to calcualte performance
            if (marketValues.Count <= 1)
                return new List<PeriodSummary>();

            // Roll up the data into some monthly summaries
            var monthlySummaries = this.GetMonthlySummaries(marketValues, cashFlows);
            

            // Inception to Date (calculates for all periods
            this.CalcualteRateOfReturn(monthlySummaries, date => false,
            (period, rateOfReturn) => period.InceptionToDateReturn = rateOfReturn);

            // Year to Date
            this.CalcualteRateOfReturn(monthlySummaries, date => date.Month == 1,
            (period, rateOfReturn) => period.YearToDateReturn = rateOfReturn);
            
            // Quarter To Date                   
            this.CalcualteRateOfReturn(monthlySummaries, date => date.MonthInQuarter() == 1,
            (period, rateOfReturn) => period.QuarterToDateReturn = rateOfReturn);
            
            return monthlySummaries.Select(ms => new PeriodSummary()
            {
                PeriodNumber = ms.PeriodNumber,
                StartingDate = ms.StartingDate,
                EndingDate = ms.EndingDate,
                TotalCashFlows = ms.TotalCashFlows,
                StartingMarketValue = ms.StartingMarketValue,
                EndingMarketValue = ms.EndingMarketValue,
                MonthlyRateOfReturn = ms.MonthlyRateOfReturn,
                QuarterToDateReturn = ms.QuarterToDateReturn.GetValueOrDefault(),
                YearToDateReturn = ms.YearToDateReturn.GetValueOrDefault(),
                InceptionToDateReturn = ms.InceptionToDateReturn.GetValueOrDefault()
            }).ToList();            
        }


        internal List<AccountMarketValue> GetAccountMarketValues(String accountNumber)
        {
            return this.marketValueRepository.LoadAccountMarketValues(accountNumber)
                .OrderBy(mv => mv.Date)
                .ToList();
        }


        internal List<CashFlow> GetAccountCashFlows(String accountNumber)
        {
             return this.cashFlowRepository.LoadExternalCashFlows(accountNumber);
        }


        internal List<PeriodInfo> GetMonthlySummaries(List<AccountMarketValue> marketValues, List<CashFlow> cashFlows)
        {
            // We want to get the market values for all the month ends plus the first day the
            // account had funds and last day the account had funds to calc performance
            var firstMarketValueDate = marketValues.First().Date;
            var lastMarketValueDate = marketValues.Last().Date;
            var perfMarketValues = marketValues
                .Where(mv => mv.TradeDate.IsMonthEnd
                    || mv.Date.IsSameDate(firstMarketValueDate)
                    || mv.Date.IsSameDate(lastMarketValueDate))
                .ToList();

            // This creates the performance periods.  The first and last period are generally
            // partial months
            List<PeriodInfo> monthlySummaries = new List<PeriodInfo>();
                for (int i = 1; i < perfMarketValues.Count; i++)
                {
                    var isFirstPeriod = (i == 1);
                    var isLastPeriod = (i == perfMarketValues.Count - 1);

                    var startingMarketValue = perfMarketValues[i - 1];
                    var endingMarketValue = perfMarketValues[i];

                    PeriodInfo period = new PeriodInfo()
                    {
                        PeriodNumber = i,
                        StartingDate = (i == 1) ? startingMarketValue.Date : endingMarketValue.Date.FirstDayOfMonth(),
                        StartingMarketValue = (i == 1) ? 0.0m : startingMarketValue.MarketValue,
                        EndingDate = (isLastPeriod && !endingMarketValue.TradeDate.IsMonthEnd)
                            ? endingMarketValue.Date : endingMarketValue.Date.LastDayOfMonth(),
                        EndingMarketValue = endingMarketValue.MarketValue
                    };
                    monthlySummaries.Add(period);
                }
            

            // Now assign the Cash Flows to each period
            foreach (var cashFlow in cashFlows)
            {
                var period = monthlySummaries
                    .First(pp => cashFlow.Date.IsOnOrBetweenDates(pp.StartingDate, pp.EndingDate));

                period.CashFlows.Add(cashFlow.Amount);
            }
            
            return monthlySummaries;
        }


        /// <summary>
        /// Calcualtes a rate of return for all periods 
        /// </summary>
        /// <param name="monthlySummaries"></param>
        /// <param name="isNewPeriod">Function to check if this month is the start of a new performance period.
        /// For example, year to date performance is reset every January, so when calculating YTD performance
        /// you would pass in a function to see if the month was January
        /// </param>
        /// <param name="setPerformanceResult">Function to set the performance on the monthlySummary</param>
        internal void CalcualteRateOfReturn(List<PeriodInfo> monthlySummaries, 
            Func<DateTime, bool> isNewPeriod, Action<PeriodInfo, decimal> setPerformanceResult)
        {
            // Slow this function down to simulate a calculation process that takes longer
            Thread.Sleep(500);

            var geometricSum = 1.0m;
            monthlySummaries
                .Select(pp => new { period = pp, factor = (1 + pp.MonthlyRateOfReturn) })
                .ForEach(item =>
                {
                    // If we are starting a new performancePeriod, reset the geometricSum
                    geometricSum = (isNewPeriod(item.period.EndingDate)) ? 1.0m : geometricSum;                     

                    // Update the geometric sum for this period
                    geometricSum = geometricSum * item.factor;

                    var timeWeightedRateOfReturn = geometricSum - 1;
                    setPerformanceResult(item.period, timeWeightedRateOfReturn);                     
                });

        }



    }
}
