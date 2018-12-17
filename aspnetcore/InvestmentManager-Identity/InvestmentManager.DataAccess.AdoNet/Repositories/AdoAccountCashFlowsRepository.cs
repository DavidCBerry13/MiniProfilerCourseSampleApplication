using InvestmentManager.Core.Common;
using InvestmentManager.Core.DataAccess;
using InvestmentManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace InvestmentManager.DataAccess.AdoNet.Repositories
{
    internal class AdoAccountCashFlowsRepository : BaseRepository, IAccountCashFlowsRepository
    {

        internal AdoAccountCashFlowsRepository(String connectionString)
            : base(connectionString)
        {
            
        }




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

            using (IDbConnection con = this.GetConnection())
            {
                con.Open();
                using (IDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = $"{SQL} WHERE f.AccountNumber = @AccountNumber";
                    cmd.AddParameterWithValue("@accountNumber", accountNumber);

                    using (IDataReader reader = cmd.ExecuteReader())
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

            using (IDbConnection con = this.GetConnection())
            {
                con.Open();
                using (IDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = $"{SQL} WHERE f.AccountNumber = @AccountNumber AND ft.ExternalFlow = 1";
                    cmd.AddParameterWithValue("@accountNumber", accountNumber);

                    using (IDataReader reader = cmd.ExecuteReader())
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


        internal CashFlow DecodeRow(IDataReader reader)
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
