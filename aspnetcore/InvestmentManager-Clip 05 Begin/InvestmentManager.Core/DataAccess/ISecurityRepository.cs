using InvestmentManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvestmentManager.Core.DataAccess
{
    public interface ISecurityRepository
    {


        Security LoadSecurity(String symbol);


        List<Security> LoadSecurities();



    }
}
