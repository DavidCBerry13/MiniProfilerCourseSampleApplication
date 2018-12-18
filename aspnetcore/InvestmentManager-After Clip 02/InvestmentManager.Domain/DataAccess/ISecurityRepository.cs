using System;
using System.Collections.Generic;
using System.Text;

namespace InvestmentManager.Domain.DataAccess
{
    public interface ISecurityRepository
    {


        Security LoadSecurity(String symbol);


        List<Security> LoadSecurities();



    }
}
