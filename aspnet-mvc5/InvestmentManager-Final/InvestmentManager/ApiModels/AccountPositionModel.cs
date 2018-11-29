using Newtonsoft.Json;
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
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("accountNumber")]
        public String AccountNumber { get; set; }

        [JsonProperty("symbol")]
        public String Symbol { get; set; }

        [JsonProperty("securityName")]
        public String SecurityName { get; set; }

        [JsonProperty("description")]
        public String Description { get; set; }

        [JsonProperty("sector")]
        public String Sector { get; set; }

        [JsonProperty("industry")]
        public String Industry { get; set; }

        [JsonProperty("shares")]
        public decimal Shares { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("marketValue")]
        public decimal MarketValue { get; set; }

    }
}
