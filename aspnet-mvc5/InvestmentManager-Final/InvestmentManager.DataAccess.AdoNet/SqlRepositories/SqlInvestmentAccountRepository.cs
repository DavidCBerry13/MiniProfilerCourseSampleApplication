using InvestmentManager.Core.Common;
using InvestmentManager.Core.DataAccess;
using InvestmentManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace InvestmentManager.DataAccess.AdoNet.SqlRepositories
{
    internal class SqlInvestmentAccountRepository : IInvestmentAccountRepository
    {
        internal SqlInvestmentAccountRepository(String connectionString)
        {
            _connectionString = connectionString;
        }

        private String _connectionString;


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

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                String sql = $"{SQL} WHERE a.AccountNumber = @AccountNumber";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@TradeDate", tradeDate.Date);
                    cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);
                    using (SqlDataReader reader = cmd.ExecuteReader())
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

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(SQL, con))
                {
                    cmd.Parameters.AddWithValue("@TradeDate", tradeDate.Date);

                    using (SqlDataReader reader = cmd.ExecuteReader())
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


        internal InvestmentAccount DecodeRow(SqlDataReader reader)
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
