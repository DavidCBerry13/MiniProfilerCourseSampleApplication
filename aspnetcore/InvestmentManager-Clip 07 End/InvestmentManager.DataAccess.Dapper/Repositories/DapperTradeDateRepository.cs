using Dapper;
using InvestmentManager.Core.DataAccess;
using InvestmentManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace InvestmentManager.DataAccess.Dapper.Repositories
{
    internal class DapperTradeDateRepository : DapperBaseRepository, ITradeDateRepository
    {

        internal DapperTradeDateRepository(String connectionString)
            : base(connectionString)
        {
        }



        public const String ALL_TRADE_DATES_SQL =
            @"SELECT
                  TradeDate As Date, 
		          MonthEndDate AS IsMonthEnd,
		          QuarterEndDate AS IsQuarterEnd,
		          YearEndDate As IsYearEnd
	          FROM TradeDates";

        public const String CURRENT_TRADE_DATE_SQL =
            @"SELECT TOP 1
                  TradeDate AS Date, 
		          MonthEndDate AS IsMonthEnd,
		          QuarterEndDate AS IsQuarterEnd,
		          YearEndDate AS IsYearEnd
	          FROM TradeDates
              ORDER BY TradeDate DESC";



        public TradeDate GetLatestTradeDate()
        {
            using (IDbConnection con = this.GetConnection())
            {
                con.Open();
                return con.QueryFirstOrDefault<TradeDate>(CURRENT_TRADE_DATE_SQL);
            }
        }


        public List<TradeDate> LoadTradeDates()
        {
            using (IDbConnection con = this.GetConnection())
            {
                con.Open();
                return con.Query<TradeDate>(ALL_TRADE_DATES_SQL).ToList();
            }


        }



    }
}
