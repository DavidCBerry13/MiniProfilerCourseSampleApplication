using InvestmentManager.Core.Common;
using InvestmentManager.Core.DataAccess;
using InvestmentManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace InvestmentManager.DataAccess.AdoNet.Repositories
{
    internal class AdoInvestmentAccountRepository : BaseRepository, IInvestmentAccountRepository
    {
        internal AdoInvestmentAccountRepository(String connectionString)
            : base(connectionString)
        {
        }


        internal static String SQL =
            @"SELECT
        a.AccountNumber,
		a.AccountName,
		a.AccountTypeCode,
		a.TaxIdNumber,
		a.Address,
		a.City,
		a.State,
		a.ZipCode,
		a.OpenDate,
		a.CloseDate,
		at.AccountTypeName,
		at.AccountPrefix,
		mv.MarketValue
    FROM Accounts a
	INNER JOIN AccountTypes at
	    ON a.AccountTypeCode = at.AccountTypeCode
	INNER JOIN AccountMarketValues mv
	    ON a.AccountNumber = mv.AccountNumber
		AND mv.TradeDate = @TradeDate";





        public InvestmentAccount LoadInvestmentAccount(string accountNumber, TradeDate tradeDate)
        {
            InvestmentAccount account = null;

            using (IDbConnection con = this.GetConnection())
            {
                con.Open();
 
                using (IDbCommand cmd = con.CreateCommand() )
                {
                    cmd.CommandText = $"{SQL} WHERE a.AccountNumber = @AccountNumber";
                    cmd.AddParameterWithValue("@TradeDate", tradeDate.Date);
                    cmd.AddParameterWithValue("@AccountNumber", accountNumber);

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            account = this.DecodeRow(reader);                            
                        }
                    }
                }
            }

            return account;
        }

        public IEnumerable<InvestmentAccount> LoadInvestmentAccounts(TradeDate tradeDate)
        {
            List<InvestmentAccount> accounts = new List<InvestmentAccount>();

            using (IDbConnection con = this.GetConnection())
            {
                con.Open();
                using (IDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = SQL;
                    cmd.AddParameterWithValue("@TradeDate", tradeDate.Date);

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var account = this.DecodeRow(reader);
                            accounts.Add(account);
                        }
                    }
                }
            }

            return accounts;
        }


        internal InvestmentAccount DecodeRow(IDataReader reader)
        {
            var accountNumber = reader.GetString(0);
            var accountName = reader.GetString(1);
            var accountTypeCode = reader.GetString(2);
            var taxIdNumber = reader.GetString(3);
            var address = reader.GetString(4);
            var city = reader.GetString(5);
            var state = reader.GetString(6);
            var zipCode = reader.GetString(7);
            var openDate = reader.GetDateTime(8);
            var closeDate = reader.GetNullableDateTime(9);
            var accountTypeName = reader.GetString(10);
            var accountPrefix = reader.GetString(11);
            var marketValue = reader.GetDecimal(12);

            InvestmentAccountType accountType = new InvestmentAccountType()
            {
                Code = accountTypeCode,
                Name = accountTypeName,
                Prefix = accountPrefix
            };

            InvestmentAccount account = new InvestmentAccount()
            {
                AccountNumber = accountNumber,
                AccountName = accountName,
                AccountTypeCode = accountTypeCode,
                AccountType = accountType,
                Address = address,
                City = city,
                State = state,
                ZipCode = zipCode,
                OpenDate = openDate,
                CloseDate = closeDate,
                MarketValue = marketValue
            };

            return account;
        }

    }
}
