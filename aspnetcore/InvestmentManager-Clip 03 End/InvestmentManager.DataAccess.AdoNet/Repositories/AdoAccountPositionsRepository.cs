using InvestmentManager.Core.Common;
using InvestmentManager.Core.DataAccess;
using InvestmentManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace InvestmentManager.DataAccess.AdoNet.Repositories
{
    internal class AdoAccountPositionsRepository : BaseRepository, IAccountPositionsRepository
    {
        internal AdoAccountPositionsRepository(string connectionString)
            : base(connectionString)
        {
            
        }



        public const String SQL =
            @"SELECT
        p.TradeDate,
		p.AccountNumber,
		p.Ticker,
		p.Shares,
		p.Price,
		p.MarketValue,
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
    FROM AccountPositions p
	INNER JOIN Securities s
	    ON p.Ticker = s.Ticker
	INNER JOIN SecurityTypes st
	    ON s.SecurityTypeCode = st.SecurityTypeCode";



        public List<AccountPosition> LoadAccountPositions(string accountNumber, TradeDate tradeDate)
        {
            List<AccountPosition> positions = new List<AccountPosition>();

            using (IDbConnection con = this.GetConnection() )
            {
                con.Open();
                
                using (IDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = $"{SQL} WHERE p.AccountNumber = @AccountNumber AND p.TradeDate = @TradeDate";
                    cmd.AddParameterWithValue("@AccountNumber", accountNumber);
                    cmd.AddParameterWithValue("@TradeDate", tradeDate.Date);

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var position = this.DecodeRow(reader);
                            positions.Add(position);
                        }
                    }
                }
            }
            return positions;
        }



        public AccountPosition DecodeRow(IDataReader reader)
        {
            var tradeDate = reader.GetDateTime(0);
            var accountNumber = reader.GetString(1);
            var ticker = reader.GetString(2);
            var shares = reader.GetDecimal(3);
            var price = reader.GetDecimal(4);
            var marketValue = reader.GetDecimal(5);
            var securityTypeCode = reader.GetString(6);
            var securityName = reader.GetString(7);
            var exchange = reader.GetNullableString(8);
            var description = reader.GetNullableString(9);
            var ceo = reader.GetNullableString(10);
            var sector = reader.GetString(11);
            var industry = reader.GetNullableString(12);
            var website = reader.GetNullableString(13);
            var issueType = reader.GetNullableString(14);
            var securityTypeName = reader.GetString(15);

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

            AccountPosition position = new AccountPosition()
            {
                Date = tradeDate,
                AccountNumber = accountNumber,
                Symbol = ticker,
                Security = security,
                Shares = shares,
                Price = price,
                MarketValue = marketValue
            };
            return position;
        }
    }
}
