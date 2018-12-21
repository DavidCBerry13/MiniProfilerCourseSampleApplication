using System;
using System.Collections.Generic;
using System.Text;

namespace InvestmentManager.Domain
{


    /// <summary>
    /// Represents the type of Investment Account 
    /// </summary>
    public class InvestmentAccountType
    {

        public String Code { get; private set; }

        public String Name { get; private set; }

        public String Prefix { get; private set; }

    }
}
