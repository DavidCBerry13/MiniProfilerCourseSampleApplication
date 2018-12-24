using InvestmentManager.Core.Domain;
using InvestmentManager.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace InvestmentManager.DataAccess.EF.Repositories
{
    public class InvestmentAccountRepository : IInvestmentAccountRepository
    {

        public InvestmentAccountRepository(InvestmentContext context)
        {
            this.dbContext = context;
        }

        protected InvestmentContext dbContext;



        public InvestmentAccount LoadInvestmentAccount(String accountNumber, TradeDate tradeDate)
        {
            return this.dbContext.Accounts
                .Include(account => account.AccountType)
                .Where(account => account.AccountNumber == accountNumber)
                .Select(acct => new
                {
                    Account = acct,
                    MarketValue = this.dbContext.AccountMarketValues
                        .Where(mv => mv.AccountNumber == acct.AccountNumber && mv.Date == tradeDate.Date)
                        .Sum(mv => mv.MarketValue)
                }
                ).AsEnumerable()
                .Select(x =>
                {
                    x.Account.MarketValue = x.MarketValue;
                    return x.Account;
                }).FirstOrDefault();
        }


        public IEnumerable<InvestmentAccount> LoadInvestmentAccounts(TradeDate tradeDate)
        {
            //return this.dbContext.Accounts
            //    .Include(account => account.AccountType)
            //    .Include(account => account.Positions)
            //    .ThenInclude(position => position.Security)
            //    .Select(account => new
            //    {
            //        account,
            //        Positions = account.Positions
            //            .Where(p => p.Date == tradeDate.Date),
            //        MarketValue = account.Positions
            //            .Where(p => p.Date == tradeDate.Date)
            //            .Sum(p => p.MarketValue)
            //    })
            //    .AsEnumerable()
            //    .Select(x =>
            //    {
            //        x.account.Positions = x.Positions.ToList();
            //        x.account.MarketValue = x.MarketValue;
            //        return x.account;
            //    })
            //    .ToList();

            return this.dbContext.Accounts
                .Include(account => account.AccountType)
                .Select(acct => new
                {
                    Account = acct,
                    MarketValue = this.dbContext.AccountMarketValues
                        .Where(mv => mv.AccountNumber == acct.AccountNumber && mv.Date == tradeDate.Date)
                        .Sum(mv => mv.MarketValue)
                }
                ).AsEnumerable()
                .Select(x =>
                {
                    x.Account.MarketValue = x.MarketValue;
                    return x.Account;
                }).ToList();



        }
    }



}
