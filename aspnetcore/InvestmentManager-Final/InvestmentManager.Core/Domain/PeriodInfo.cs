using System;
using System.Collections.Generic;
using System.Linq;

namespace InvestmentManager.Core.Domain
{

    /// <summary>
    /// Working object used by the RateOfReturnService to calcualte performance
    /// </summary>
    /// <remarks>
    /// This object is used to gather all the information needed to calculate performance
    /// for the account over the lifetime of the account.
    /// </remarks>
    internal class PeriodInfo
    {

        public PeriodInfo()
        {
            this.CashFlows = new List<decimal>();
        }

        public int PeriodNumber { get; set; }

        public DateTime StartingDate { get; set; }

        public DateTime EndingDate { get; set; }

        public List<decimal> CashFlows { get; private set; }

        public decimal TotalCashFlows
        {
            get { return this.CashFlows.Sum(cf => cf);  }
        }

        public decimal StartingMarketValue { get; set; }

        public decimal EndingMarketValue { get; set; }

        public decimal MonthlyRateOfReturn
        {
            get
            {
                return (this.EndingMarketValue - (this.StartingMarketValue + this.TotalCashFlows))
                    / (this.StartingMarketValue + this.TotalCashFlows);
            }
        }


        public decimal? QuarterToDateReturn { get; set; }

        public decimal? YearToDateReturn { get; set; }

        public decimal? InceptionToDateReturn { get; set; }

         

    }
}
