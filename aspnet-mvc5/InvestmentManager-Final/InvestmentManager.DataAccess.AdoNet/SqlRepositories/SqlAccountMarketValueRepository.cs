using InvestmentManager.Core.DataAccess;
using InvestmentManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace InvestmentManager.DataAccess.AdoNet.SqlRepositories
{
    internal class SqlAccountMarketValueRepository : IAccountMarketValueRepository
    {

        internal SqlAccountMarketValueRepository(String connectionString)
        {
            _connectionString = connectionString;
        }

        private String _connectionString;


        internal const String SQL =
            @"SELECT 
        mv.TradeDate,
		mv.AccountNumber,
		mv.MarketValue,
		d.MonthEndDate,
		d.QuarterEndDate,
		d.YearEndDate
    FROM AccountMarketValues mv
	INNER JOIN TradeDates d
	    ON d.TradeDate = mv.TradeDate";


        public List<AccountMarketValue> LoadAccountMarketValues(string accountNumber)
        {
            List<AccountMarketValue> marketValues = new List<AccountMarketValue>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                String sql = $"{SQL} WHERE mv.AccountNumber = @AccountNumber";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var marketValue = this.DecodeRow(reader);
                            marketValues.Add(marketValue);
                        }
                    }
                }
            }

            return marketValues;
        }


        public AccountMarketValue DecodeRow(SqlDataReader reader)
        {
            var date = reader.GetDateTime(0);
            var accountNumber = reader.GetString(1);
            var marketValue = reader.GetDecimal(2);
            var isMonthEnd = reader.GetBoolean(3);
            var isQuarterEnd = reader.GetBoolean(4);
            var isYearEnd = reader.GetBoolean(5);

            TradeDate tradeDate = new TradeDate()
            {
                Date = date,
                IsMonthEnd = isMonthEnd,
                IsQuarterEnd = isQuarterEnd,
                IsYearEnd = isYearEnd
            };


            AccountMarketValue accountMarketValue = new AccountMarketValue()
            {
                Date = date,
                TradeDate = tradeDate,
                AccountNumber = accountNumber,
                MarketValue = marketValue
            };

            return accountMarketValue;
        }

    }
}
