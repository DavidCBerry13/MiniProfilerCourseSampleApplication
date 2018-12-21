using Dapper;
using InvestmentManager.Core.Common;
using InvestmentManager.Core.DataAccess;
using InvestmentManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace InvestmentManager.DataAccess.Dapper.Repositories
{
    internal class DapperSecurityRepository : DapperBaseRepository, ISecurityRepository
    {


        internal DapperSecurityRepository(String connectionString)
            : base(connectionString)
        {
        }


        public const string SQL =
            @"SELECT 
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
    FROM Securities s
	INNER JOIN SecurityTypes st
	    ON s.SecurityTypeCode = st.SecurityTypeCode";






        public Security LoadSecurity(string symbol)
        {

            var sql = $"{SQL} WHERE s.Ticker = @Ticker";

            using (IDbConnection con = this.GetConnection())
            {
                con.Open();
                var data = con.Query<Security, SecurityType, Security>(sql,
                    map: (s, t) => { s.SecurityType = t; return s; },
                    splitOn: "SecurityTypeCode",
                    param: new { ticker = symbol });
                return data.FirstOrDefault();
            }
        }


        public List<Security> LoadSecurities()
        {
            var sql = SQL;

            using (IDbConnection con = this.GetConnection())
            {
                con.Open();
                var data = con.Query<Security, SecurityType, Security>(sql,
                    map: (s, t) => { s.SecurityType = t; return s; },
                    splitOn: "SecurityTypeCode");
                return data.ToList();
            }
        }

    }
}
