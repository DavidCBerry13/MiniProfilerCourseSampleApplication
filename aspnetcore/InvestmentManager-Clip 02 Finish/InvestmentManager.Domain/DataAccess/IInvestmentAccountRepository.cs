using System;
using System.Collections.Generic;
using System.Text;

namespace InvestmentManager.Domain.DataAccess
{



    public interface IInvestmentAccountRepository
    {


        IEnumerable<InvestmentAccount> LoadInvestmentAccounts();


        InvestmentAccount LoadInvestmentAccount(String accountNumber);



    }
}
