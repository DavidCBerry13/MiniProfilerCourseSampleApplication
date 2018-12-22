using InvestmentManager.Core.DataAccess;
using InvestmentManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace InvestmentManager.DataAccess.EF.Repositories
{
    public class AccountMarketValueRepository : IAccountMarketValueRepository
    {

        public AccountMarketValueRepository(InvestmentContext context)
        {
            this.dbContext = context;
        }

        protected InvestmentContext dbContext;


        public List<AccountMarketValue> LoadAccountMarketValues(string accountNumber)
        {
            return this.dbContext.AccountMarketValues
                .Include(mv => mv.TradeDate)
                .Where(mv => mv.AccountNumber == accountNumber)
                .ToList();
        }
    }
}
