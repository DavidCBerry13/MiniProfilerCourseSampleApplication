using InvestmentManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestmentManager.Models
{
    public class InvestmentAccountDetailsModel
    {

        public TradeDate TradeDate { get; set; }

        public InvestmentAccount InvestmentAccount { get; set; }

        public PeriodSummary CurrentPerformance { get; set; }
    }
}
