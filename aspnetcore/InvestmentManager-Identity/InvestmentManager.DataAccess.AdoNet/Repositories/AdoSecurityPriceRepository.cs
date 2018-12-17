using InvestmentManager.Core.Common;
using InvestmentManager.Core.DataAccess;
using InvestmentManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace InvestmentManager.DataAccess.AdoNet.Repositories
{
    internal class AdoSecurityPriceRepository : BaseRepository, ISecurityPriceRepository
    {

        internal AdoSecurityPriceRepository(String connectionString)
            : base(connectionString)
        {
        }


        internal const String SQL =
            @"SELECT
        p.TradeDate,
		p.Ticker,
		p.OpenPrice,
		p.ClosePrice,
		p.DailyHigh,
		p.DailyLow,
		p.Volume,
		p.Change,
		p.ChangePercent,
		d.MonthEndDate,
		d.QuarterEndDate,
		d.YearEndDate,
	    s.SecurityTypeCode, 
		s.SecurityName, 
		s.Exchange, 
		s.Description, 
		s.Ceo, 
		s.Sector, 
		s.Industry, 
		s.Website,
		s.IssueType,
		st.SecurityTypeName
    FROM SecurityPrices p
	INNER JOIN TradeDates d
	    ON p.TradeDate = d.TradeDate
	INNER JOIN Securities s
	    ON p.Ticker = s.Ticker
	INNER JOIN SecurityTypes st
	    ON s.SecurityTypeCode = st.SecurityTypeCode";


        public List<SecurityPrice> LoadSecurityPrices(TradeDate date)
        {
            List<SecurityPrice> prices = new List<SecurityPrice>();

            using (IDbConnection con = this.GetConnection())
            {
                con.Open();
                using (IDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = $"{SQL} WHERE p.TradeDate = @TradeDate";

                    cmd.AddParameterWithValue("@TradeDate", date.Date);
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var price = this.DecodeRow(reader);
                            prices.Add(price);
                        }
                    }
                }
            }

            return prices;
        }

        public List<SecurityPrice> LoadSecurityPrices(string symbol)
        {
            List<SecurityPrice> prices = new List<SecurityPrice>();

            using (IDbConnection con = this.GetConnection())
            {
                con.Open();              
                using (IDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = $"{SQL} WHERE p.Ticker = @Symbol";
                    cmd.AddParameterWithValue("@Symbol", symbol);
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var price = this.DecodeRow(reader);
                            prices.Add(price);
                        }
                    }
                }
            }

            return prices;
        }


        public SecurityPrice DecodeRow(IDataReader reader)
        {
            var date = reader.GetDateTime(0);
            var ticker = reader.GetString(1);
            var openPrice = reader.GetDecimal(2);
            var closePrice = reader.GetDecimal(3);
            var dailyHigh = reader.GetDecimal(4);
            var dailyLow = reader.GetDecimal(5);
            var volume = reader.GetInt64(6);
            var change = reader.GetDecimal(7);
            var changePercent = reader.GetDecimal(8);
            var isMonthEnd = reader.GetBoolean(9);
            var isQuarterEnd = reader.GetBoolean(10);
            var isYearEnd = reader.GetBoolean(11);
            var securityTypeCode = reader.GetString(12);
            var securityName = reader.GetString(13);
            var exchange = reader.GetNullableString(14);
            var description = reader.GetNullableString(15);
            var ceo = reader.GetNullableString(16);
            var sector = reader.GetString(17);
            var industry = reader.GetNullableString(18);
            var website = reader.GetNullableString(19);
            var issueType = reader.GetNullableString(20);
            var securityTypeName = reader.GetString(21);

            TradeDate tradeDate = new TradeDate()
            {
                Date = date,
                IsMonthEnd = isMonthEnd,
                IsQuarterEnd = isQuarterEnd,
                IsYearEnd = isYearEnd
            };

            SecurityType securityType = new SecurityType()
            {
                Code = securityTypeCode,
                Name = securityTypeName
            };

            Security security = new Security()
            {
                Symbol = ticker,
                SecurityType = securityType,
                Name = securityName,
                Exchange = exchange,
                Description = description,
                Ceo = ceo,
                Sector = sector,
                Industry = industry,
                Website = website,
                SecurityTypeCode = securityTypeCode
            };

            SecurityPrice securityPrice = new SecurityPrice()
            {
                Date = date,
                TradeDate = tradeDate,
                Symbol = ticker,
                Security = security,
                ClosePrice = closePrice,
                OpenPrice = openPrice,
                DailyHigh = dailyHigh,
                DailyLow = dailyLow,
                Volume = volume,
                Change = change,
                ChangePercent = changePercent
            };

            return securityPrice;
        }


    }
}
