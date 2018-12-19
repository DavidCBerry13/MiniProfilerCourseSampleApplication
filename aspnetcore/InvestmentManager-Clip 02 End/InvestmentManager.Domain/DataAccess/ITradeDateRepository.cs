using System;
using System.Collections.Generic;
using System.Text;

namespace InvestmentManager.Domain.DataAccess
{
    public interface ITradeDateRepository
    {


        List<TradeDate> LoadTradeDates();


    }
}
