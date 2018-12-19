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
    internal class DapperSecurityPriceRepository : DapperBaseRepository, ISecurityPriceRepository
    {

        internal DapperSecurityPriceRepository(String connectionString)
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
        d.TradeDate As Date,
		d.MonthEndDate AS IsMonthEnd,
		d.QuarterEndDate AS IsQuarterEnd,
		d.YearEndDate AS IsYearEnd,
        s.Ticker,
	    s.SecurityTypeCode, 
		s.SecurityName, 
		s.Exchange, 
		s.Description, 
		s.Ceo, 
		s.Sector, 
		s.Industry, 
		s.Website,
		s.IssueType,
        st.SecurityTypeCode,
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
            var sql = $"{SQL} WHERE p.TradeDate = @TradeDate";

            using (IDbConnection con = this.GetConnection())
            {
                con.Open();
                var data = con.Query<SecurityPrice, TradeDate, Security, SecurityType, SecurityPrice>(sql,
                    map: (p, d, s, t) => 
                    {
                        p.TradeDate = d;
                        s.SecurityType = t;
                        p.Security = s;
                        return p;
                    },
                    splitOn: "Date, Ticker, SecurityTypeCode",
                    param: new { TradeDate = date });
                return data.ToList();
            }
        }


        public List<SecurityPrice> LoadSecurityPrices(string symbol)
        {
            var sql = $"{SQL} WHERE p.Ticker = @Symbol";

            using (IDbConnection con = this.GetConnection())
            {
                con.Open();
                var data = con.Query<SecurityPrice, TradeDate, Security, SecurityType, SecurityPrice>(sql,
                    map: (p, d, s, t) =>
                    {
                        p.TradeDate = d;
                        s.SecurityType = t;
                        p.Security = s;
                        return p;
                    },
                    splitOn: "Date, Ticker, SecurityTypeCode",
                    param: new { Symbol = symbol });
                return data.ToList();
            }

        }

    }
}
