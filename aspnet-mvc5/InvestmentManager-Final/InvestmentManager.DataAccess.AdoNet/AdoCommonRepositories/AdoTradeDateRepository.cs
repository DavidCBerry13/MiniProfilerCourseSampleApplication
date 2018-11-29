using InvestmentManager.Core.DataAccess;
using InvestmentManager.Core.Domain;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace InvestmentManager.DataAccess.AdoNet.AdoCommonRepositories
{
    internal class AdoTradeDateRepository : BaseRepository, ITradeDateRepository
    {

        internal AdoTradeDateRepository(String connectionString)
            :base(connectionString)
        {
        }



        public const String ALL_TRADE_DATES_SQL =
            @"SELECT
                  TradeDate, 
		          MonthEndDate,
		          QuarterEndDate,
		          YearEndDate
	          FROM TradeDates";

        public const String CURRENT_TRADE_DATE_SQL =
            @"SELECT TOP 1
                  TradeDate, 
		          MonthEndDate,
		          QuarterEndDate,
		          YearEndDate
	          FROM TradeDates
              ORDER BY TradeDate DESC";



        public TradeDate GetLatestTradeDate()
        {
            TradeDate tradeDate = null;

            using (IDbConnection con = this.GetConnection())
            {
                con.Open();

                using (IDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = CURRENT_TRADE_DATE_SQL;
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tradeDate = this.DecodeRow(reader);
                        }
                    }
                }
            }

            return tradeDate;
        }

        public List<TradeDate> LoadTradeDates()
        {
            List<TradeDate> tradeDates = new List<TradeDate>();

            using (IDbConnection con = this.GetConnection())
            {
                con.Open();                
                using (IDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = ALL_TRADE_DATES_SQL;
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var date = this.DecodeRow(reader);
                            tradeDates.Add(date);
                        }
                    }
                }
            }

            return tradeDates;
        }


        public TradeDate DecodeRow(IDataReader reader)
        {
            var date = reader.GetDateTime(0);
            var isMonthEnd = reader.GetBoolean(1);
            var isQuarterEnd = reader.GetBoolean(2);
            var isYearEnd = reader.GetBoolean(3);

            TradeDate tradeDate = new TradeDate()
            {
                Date = date,
                IsMonthEnd = isMonthEnd,
                IsQuarterEnd = isQuarterEnd,
                IsYearEnd = isYearEnd
            };

            return tradeDate;
        }
    }
}
