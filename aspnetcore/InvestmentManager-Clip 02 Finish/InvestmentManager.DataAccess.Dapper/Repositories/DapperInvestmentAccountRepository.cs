using Dapper;
using InvestmentManager.Core.DataAccess;
using InvestmentManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace InvestmentManager.DataAccess.Dapper.Repositories
{
    internal class DapperInvestmentAccountRepository : DapperBaseRepository, IInvestmentAccountRepository
    {

        internal DapperInvestmentAccountRepository(String connectionString)
    : base(connectionString)
        {
        }


        internal static String SQL =
            @"SELECT
        a.AccountNumber,
		a.AccountName,
		a.AccountTypeCode,
		a.TaxIdNumber,
		a.Address,
		a.City,
		a.State,
		a.ZipCode,
		a.OpenDate,
		a.CloseDate,
        mv.MarketValue,
        at.AccountTypeCode AS Code,
		at.AccountTypeName AS Name,
		at.AccountPrefix AS Prefix		
    FROM Accounts a
	INNER JOIN AccountTypes at
	    ON a.AccountTypeCode = at.AccountTypeCode
	INNER JOIN AccountMarketValues mv
	    ON a.AccountNumber = mv.AccountNumber
		AND mv.TradeDate = @TradeDate";



        public IEnumerable<InvestmentAccount> LoadInvestmentAccounts(TradeDate tradeDate)
        {
            var sql = SQL;

            using (IDbConnection con = this.GetConnection())
            {
                con.Open();
                var data = con.Query<InvestmentAccount, InvestmentAccountType, InvestmentAccount>(sql,
                    map: (a, t) =>
                    {
                        a.AccountType = t;
                        return a;
                    },
                    splitOn: "Code",
                    param: new { TradeDate = tradeDate.Date });
                return data;
            }
        }



        public InvestmentAccount LoadInvestmentAccount(string accountNumber, TradeDate tradeDate)
        {
            var sql = $"{SQL} WHERE a.AccountNumber = @AccountNumber";

            using (IDbConnection con = this.GetConnection())
            {
                con.Open();
                var data = con.Query<InvestmentAccount, InvestmentAccountType, InvestmentAccount>(sql,
                    map: (a, t) =>
                    {
                        a.AccountType = t;
                        return a;
                    },
                    splitOn: "Code",
                    param: new { TradeDate = tradeDate.Date, AccountNumber = accountNumber });
                return data.FirstOrDefault();
            }
        }
    }

}
