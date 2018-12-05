using System;
using System.Collections.Generic;
using System.Text;

namespace InvestmentManager.Core.Domain
{
    public class AccountMarketValue
    {

        public DateTime Date { get; set; }

        public virtual TradeDate TradeDate { get; set; }

        public String AccountNumber { get; set; }

        public decimal MarketValue { get; set; }

    }
}
