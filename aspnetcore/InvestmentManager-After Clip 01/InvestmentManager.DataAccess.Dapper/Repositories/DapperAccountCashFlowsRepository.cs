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
    internal class DapperAccountCashFlowsRepository : DapperBaseRepository, IAccountCashFlowsRepository
    {

        internal DapperAccountCashFlowsRepository(String connectionString)
            : base(connectionString)
        {

        }




        internal const string SQL =
            @"SELECT
                  f.AccountCashFlowId As CashFlowId,		          
		          f.TradeDate AS Date,
                  f.AccountNumber,
		          f.CashFlowTypeCode, 
		          f.Amount,
		          f.Description,
                  ft.CashFlowTypeCode AS Code,
		          ft.CashFlowTypeName AS Name,
		          ft.ExternalFlow AS IsExternal
        FROM AccountCashFlows f
	    INNER JOIN CashFlowTypes ft
	        ON f.CashFlowTypeCode = ft.CashFlowTypeCode";


        public List<CashFlow> LoadAllAccountCashFlows(string accountNumber)
        {
            var sql = $"{SQL} WHERE f.AccountNumber = @AccountNumber";

            using (IDbConnection con = this.GetConnection())
            {
                con.Open();
                var data = con.Query<CashFlow, CashFlowType, CashFlow>(sql,
                    map: (f, t) =>
                    {
                        f.CashFlowType = t;
                        return f;
                    },
                    splitOn: "Code",
                    param: new { AccountNumber = accountNumber });
                return data.ToList();
            }
        }

        public List<CashFlow> LoadExternalCashFlows(string accountNumber)
        {
            var sql = $"{SQL} WHERE f.AccountNumber = @AccountNumber AND ft.ExternalFlow = 1";

            using (IDbConnection con = this.GetConnection())
            {
                con.Open();
                var data = con.Query<CashFlow, CashFlowType, CashFlow>(sql,
                    map: (f, t) =>
                    {
                        f.CashFlowType = t;
                        return f;
                    },
                    splitOn: "Code",
                    param: new { AccountNumber = accountNumber });
                return data.ToList();
            }
        }
    }
}
