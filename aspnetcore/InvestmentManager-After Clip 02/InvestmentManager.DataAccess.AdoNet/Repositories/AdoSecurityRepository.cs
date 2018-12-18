using InvestmentManager.Core.Common;
using InvestmentManager.Core.DataAccess;
using InvestmentManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace InvestmentManager.DataAccess.AdoNet.Repositories
{
    internal class AdoSecurityRepository : BaseRepository, ISecurityRepository
    {
        internal AdoSecurityRepository(String connectionString)
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
		st.SecurityTypeName
    FROM Securities s
	INNER JOIN SecurityTypes st
	    ON s.SecurityTypeCode = st.SecurityTypeCode";



 


        public Security LoadSecurity(string symbol)
        {
            Security security = null;

            using (IDbConnection con = this.GetConnection())
            {
                con.Open();                
                using (IDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = $"{SQL} WHERE s.Ticker = @Ticker";
                    cmd.AddParameterWithValue("@Ticker", symbol);
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            security = this.DecodeRow(reader);                            
                        }
                    }
                }
            }

            return security;
        }

        public List<Security> LoadSecurities()
        {
            List<Security> securities = new List<Security>();

            using (IDbConnection con = this.GetConnection())
            {
                con.Open();
                using (IDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = SQL;
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var security = this.DecodeRow(reader);
                            securities.Add(security);
                        }
                    }
                }
            }

            return securities;
        }


        public Security DecodeRow(IDataReader reader)
        {
            var ticker = reader.GetString(0);
            var securityTypeCode = reader.GetString(1);
            var securityName = reader.GetString(2);
            var exchange = reader.GetNullableString(3);
            var description = reader.GetNullableString(4);
            var ceo = reader.GetNullableString(5);
            var sector = reader.GetString(6);
            var industry = reader.GetNullableString(7);
            var website = reader.GetNullableString(8);
            var issueType = reader.GetNullableString(9);
            var securityTypeName = reader.GetString(10);

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


            return security;
        }

    }
}
