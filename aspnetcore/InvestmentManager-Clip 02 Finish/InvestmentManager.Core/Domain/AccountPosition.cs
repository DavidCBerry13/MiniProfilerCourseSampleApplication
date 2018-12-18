using System;
using System.Collections.Generic;
using System.Text;

namespace InvestmentManager.Core.Domain
{


    /// <summary>
    /// Represents the holding of a security in an account on a given day
    /// </summary>
    /// <remarks>
    /// That is, Account 123 held 100 shares of MSFT on this date
    /// </remarks>
    public class AccountPosition
    {

        public DateTime Date { get; set; }

        public String AccountNumber { get; set; }

        public String Symbol { get; set; }

        public virtual Security Security { get; set; }

        public decimal Shares { get; set; }

        public decimal Price { get; set; }

        public decimal MarketValue { get; set; }

    }
}
