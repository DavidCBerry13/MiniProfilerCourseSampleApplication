using InvestmentManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvestmentManager.Core.DataAccess
{
    public interface ISecurityPriceRepository
    {


        List<SecurityPrice> LoadSecurityPrices(TradeDate date);


        List<SecurityPrice> LoadSecurityPrices(String symbol);



    }
}
