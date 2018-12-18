using InvestmentManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvestmentManager.Core.DataAccess
{
    public interface IAccountPositionsRepository
    {


        List<AccountPosition> LoadAccountPositions(String accountNumber, TradeDate tradeDate);


    }
}
