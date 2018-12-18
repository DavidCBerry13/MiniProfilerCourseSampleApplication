using System;
using System.Collections.Generic;
using System.Text;

namespace InvestmentManager.Core.Domain
{


    /// <summary>
    /// Represents the type of Investment Account 
    /// </summary>
    public class InvestmentAccountType
    {

        public String Code { get; set; }

        public String Name { get; set; }

        public String Prefix { get; set; }

    }
}
