using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestmentManager.ApiModels
{

    /// <summary>
    /// Represents the position in a security in an account (basically a stock the account holds)
    /// </summary>
    public class AccountPositionModel
    {
        public DateTime Date { get; set; }

        public String AccountNumber { get; set; }

        public String Symbol { get; set; }

        public String SecurityName { get; set; }

        public String Description { get; set; }

        public String Sector { get; set; }

        public String Industry { get; set; }

        public decimal Shares { get; set; }

        public decimal Price { get; set; }

        public decimal MarketValue { get; set; }

    }
}
