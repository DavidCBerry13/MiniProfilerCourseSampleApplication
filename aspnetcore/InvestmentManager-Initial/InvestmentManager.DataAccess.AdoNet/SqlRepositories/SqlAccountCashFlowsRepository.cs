using InvestmentManager.Core.DataAccess;
using InvestmentManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace InvestmentManager.DataAccess.AdoNet.SqlRepositories
{
    internal class SqlAccountCashFlowsRepository : IAccountCashFlowsRepository
    {

        internal SqlAccountCashFlowsRepository(String connectionString)
        {
            _connectionString = connectionString;
        }

        private String _connectionString;



        internal const string SQL =
            @"SELECT
                  f.AccountCashFlowId,
		          f.AccountNumber,
		          f.TradeDate,
		          f.CashFlowTypeCode, 
		          f.Amount,
		          f.Description,
		          ft.CashFlowTypeName,
		          ft.ExternalFlow
        FROM AccountCashFlows f
	    INNER JOIN CashFlowTypes ft
	        ON f.CashFlowTypeCode = ft.CashFlowTypeCode";


        public List<CashFlow> LoadAllAccountCashFlows(string accountNumber)
        {
            List<CashFlow> cashFlows = new List<CashFlow>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                String sql = $"{SQL} WHERE f.AccountNumber = @AccountNumber";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@accountNumber", accountNumber);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var cashFlow = this.DecodeRow(reader);
                            cashFlows.Add(cashFlow);
                        }
                    }
                }
            }

            return cashFlows;
        }


        public List<CashFlow> LoadExternalCashFlows(string accountNumber)
        {
            List<CashFlow> cashFlows = new List<CashFlow>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                String sql = $"{SQL} WHERE f.AccountNumber = @AccountNumber AND ft.ExternalFlow = 1";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@accountNumber", accountNumber);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var cashFlow = this.DecodeRow(reader);
                            cashFlows.Add(cashFlow);
                        }
                    }
                }
            }

            return cashFlows;
        }


        internal CashFlow DecodeRow(SqlDataReader reader)
        {
            var id = reader.GetInt32(0);
            var accountNumber = reader.GetString(1);
            var date = reader.GetDateTime(2);
            var cashFlowTypeCode = reader.GetString(3);
            var amount = reader.GetDecimal(4);
            var description = reader.GetString(5);
            var cashFlowTypeName = reader.GetString(6);
            var externalFlow = reader.GetBoolean(7);

            var cashFlowType = new CashFlowType()
            {
                Code = cashFlowTypeCode,
                Name = cashFlowTypeName,
                IsExternal = externalFlow
            };

            var cashFlow = new CashFlow()
            {
                CashFlowId = id,
                AccountNumber = accountNumber,
                Date = date,
                CashFlowTypeCode = cashFlowTypeCode,
                CashFlowType = cashFlowType,
                Amount = amount,
                Description = description
            };

            return cashFlow;
        }

    }
}
