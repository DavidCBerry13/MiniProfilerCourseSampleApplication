using System;
using System.Collections.Generic;
using System.Text;

namespace InvestmentManager.Domain
{

    /// <summary>
    /// Represents a Security (a Stock or Cash) that can be held in an investment account
    /// </summary>
    public class Security
    {


        public String Symbol { get; private set; }

        public String SecurityTypeCode { get; private set; }

        public SecurityType SecurityType { get; private set; }

        public String Name { get; private set; }

        public String Exchange { get; private set; }

        public String Description { get; private set; }

        public String Ceo { get; private set; }

        public String Sector { get; private set; }

        public String Industry { get; private set; }

        public String Website { get; private set; }



    }
}
