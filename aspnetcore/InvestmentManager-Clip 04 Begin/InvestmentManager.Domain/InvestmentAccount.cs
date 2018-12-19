using System;
using System.Collections.Generic;
using System.Text;

namespace InvestmentManager.Domain
{

    /// <summary>
    /// Represents an investment account for a client
    /// </summary>
    public class InvestmentAccount
    {

        public String AccountNumber { get; set; }

        public String AccountName { get; set; }

        public String AccountTypeCode { get; set; }

        public virtual InvestmentAccountType AccountType { get; set; }

        public String TaxIdNumber { get; set; }

        public String Address { get; set; }

        public String City { get; set; }

        public String State { get; set; }

        public String ZipCode { get; set; }

        public DateTime OpenDate { get; set; }

        public DateTime CloseDate { get; set; }

    }
}
