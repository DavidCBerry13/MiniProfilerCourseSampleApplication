using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockIndexWebService.Domain
{
    public class StockIndexPrice
    {

        public String IndexCode { get; set; }
        public virtual StockIndex Index { get; set; }
        public DateTime TradeDate { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal AdjustedClosePrice { get; set; }
        public long Volume { get; set; }
        public decimal Change { get; set; }
        public decimal ChangePercent { get; set; }
    }
}
