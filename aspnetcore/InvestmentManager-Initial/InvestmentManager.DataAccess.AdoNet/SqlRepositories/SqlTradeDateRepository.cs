using InvestmentManager.Core.DataAccess;
using InvestmentManager.Core.Domain;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace InvestmentManager.DataAccess.AdoNet.SqlRepositories
{
    internal class SqlTradeDateRepository : ITradeDateRepository
    {

        internal SqlTradeDateRepository(String connectionString)
        {
            _connectionString = connectionString;
        }

        private String _connectionString;


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

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(CURRENT_TRADE_DATE_SQL, con))
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

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                String sql = ALL_TRADE_DATES_SQL;
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
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
