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
    internal class DapperAccountPositionsRepository : DapperBaseRepository, IAccountPositionsRepository
    {

        internal DapperAccountPositionsRepository(string connectionString)
            : base(connectionString)
        {

        }



        public const String SQL =
            @"SELECT
        p.TradeDate AS Date,
		p.AccountNumber,
		p.Ticker AS Symbol,
		p.Shares,
		p.Price,
		p.MarketValue,
        s.Ticker AS Symbol,
		s.SecurityTypeCode, 
		s.SecurityName AS Name, 
		s.Exchange, 
		s.Description, 
		s.Ceo, 
		s.Sector, 
		s.Industry, 
		s.Website,
		s.IssueType,
        st.SecurityTypeCode AS Code,
		st.SecurityTypeName AS Name
    FROM AccountPositions p
	INNER JOIN Securities s
	    ON p.Ticker = s.Ticker
	INNER JOIN SecurityTypes st
	    ON s.SecurityTypeCode = st.SecurityTypeCode";


        public List<AccountPosition> LoadAccountPositions(string accountNumber, TradeDate tradeDate)
        {
            var sql = $"{SQL} WHERE p.AccountNumber = @AccountNumber AND p.TradeDate = @TradeDate";

            using (IDbConnection con = this.GetConnection())
            {
                con.Open();
                var data = con.Query<AccountPosition, Security, SecurityType, AccountPosition>(sql,
                    map: (p, s, t) =>
                    {
                        s.SecurityType = t;
                        p.Security = s;
                        return p;
                    },
                    splitOn: "Symbol, Code",
                    param: new { TradeDate = tradeDate.Date, AccountNumber = accountNumber });
                return data.ToList();
            }
        }
    }
}
