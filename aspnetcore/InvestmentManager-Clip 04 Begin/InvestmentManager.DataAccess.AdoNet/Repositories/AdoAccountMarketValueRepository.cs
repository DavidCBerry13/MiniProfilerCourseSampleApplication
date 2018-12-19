using InvestmentManager.Core.Common;
using InvestmentManager.Core.DataAccess;
using InvestmentManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace InvestmentManager.DataAccess.AdoNet.Repositories
{
    internal class AdoAccountMarketValueRepository : BaseRepository, IAccountMarketValueRepository
    {

        internal AdoAccountMarketValueRepository(String connectionString)
            : base(connectionString)
        {
            
        }



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

            using (IDbConnection con = this.GetConnection())
            {
                con.Open();
                using (IDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = $"{SQL} WHERE mv.AccountNumber = @AccountNumber";
                    cmd.AddParameterWithValue("@AccountNumber", accountNumber);

                    using (IDataReader reader = cmd.ExecuteReader())
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


        public AccountMarketValue DecodeRow(IDataReader reader)
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
