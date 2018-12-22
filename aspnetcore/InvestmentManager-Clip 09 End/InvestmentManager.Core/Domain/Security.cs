using System;
using System.Collections.Generic;
using System.Text;

namespace InvestmentManager.Core.Domain
{

    /// <summary>
    /// Represents a Security (a Stock or Cash) that can be held in an investment account
    /// </summary>
    public class Security
    {


        public String Symbol { get; set; }

        public String SecurityTypeCode { get; set; }

        public virtual SecurityType SecurityType { get; set; }

        public String Name { get; set; }

        public String Exchange { get; set; }

        public String Description { get; set; }

        public String Ceo { get; set; }

        public String Sector { get; set; }

        public String Industry { get; set; }

        public String Website { get; set; }



    }
}
