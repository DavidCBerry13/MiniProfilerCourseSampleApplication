using InvestmentManager.Core.Domain;
using System;
using System.Collections.Generic;

namespace InvestmentManager.Core.DataAccess
{



    public interface IInvestmentAccountRepository
    {


        IEnumerable<InvestmentAccount> LoadInvestmentAccounts(TradeDate tradeDate);


        InvestmentAccount LoadInvestmentAccount(String accountNumber, TradeDate tradeDate);



    }
}
