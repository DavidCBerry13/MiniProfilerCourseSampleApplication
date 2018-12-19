using System;
using System.Collections.Generic;
using System.Text;

namespace InvestmentManager.Core.Domain
{
    public class PeriodSummary
    {

        public int PeriodNumber { get; set; }

        public DateTime StartingDate { get; set; }

        public DateTime EndingDate { get; set; }

        public decimal TotalCashFlows { get; set; }

        public decimal StartingMarketValue { get; set; }

        public decimal EndingMarketValue { get; set; }

        public decimal MonthlyRateOfReturn { get; set; }

        public decimal QuarterToDateReturn { get; set; }

        public decimal YearToDateReturn { get; set; }

        public decimal InceptionToDateReturn { get; set; }

    }
}
