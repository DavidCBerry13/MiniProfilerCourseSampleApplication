using InvestmentManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvestmentManager.Core.DataAccess
{
    public interface IAccountCashFlowsRepository
    {

        List<CashFlow> LoadAllAccountCashFlows(String accountNumber);


        List<CashFlow> LoadExternalCashFlows(String accountNumber);
    }
}
