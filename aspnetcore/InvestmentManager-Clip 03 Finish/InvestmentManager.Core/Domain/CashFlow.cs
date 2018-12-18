using System;
using System.Collections.Generic;
using System.Text;

namespace InvestmentManager.Core.Domain
{
    public class CashFlow
    {
        public int CashFlowId { get; set; }

        public DateTime Date { get; set; }

        public String AccountNumber { get; set; }

        public String CashFlowTypeCode { get; set; }

        public virtual CashFlowType CashFlowType { get; set; }

        public decimal Amount { get; set; }

        public String Description { get; set; }

    }
}
