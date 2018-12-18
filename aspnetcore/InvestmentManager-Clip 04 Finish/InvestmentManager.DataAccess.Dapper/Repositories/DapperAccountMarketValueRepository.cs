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
    internal class DapperAccountMarketValueRepository : DapperBaseRepository, IAccountMarketValueRepository
    {

        internal DapperAccountMarketValueRepository(String connectionString)
            : base(connectionString)
        {

        }


        internal const String SQL =
            @"SELECT 
        mv.TradeDate AS Date,
		mv.AccountNumber,
		mv.MarketValue,
        d.TradeDate AS Date,
		d.MonthEndDate AS IsMonthEnd,
		d.QuarterEndDate AS IsQuarterEnd,
		d.YearEndDate AS IsYearEnd
    FROM AccountMarketValues mv
	INNER JOIN TradeDates d
	    ON d.TradeDate = mv.TradeDate";


        public List<AccountMarketValue> LoadAccountMarketValues(string accountNumber)
        {
            var sql = $"{SQL} WHERE mv.AccountNumber = @AccountNumber";

            using (IDbConnection con = this.GetConnection())
            {
                con.Open();
                var data = con.Query<AccountMarketValue, TradeDate, AccountMarketValue>(sql,
                    map: (mv, d) =>
                    {
                        mv.TradeDate = d;
                        return mv;
                    },
                    splitOn: "Date",
                    param: new {AccountNumber = accountNumber });
                return data.ToList();
            }
        }
    }
}
