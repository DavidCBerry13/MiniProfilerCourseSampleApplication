using InvestmentManager.Core.Domain;
using InvestmentManager.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvestmentManager.DataAccess.EF.Repositories
{
    public class SecurityRepository : ISecurityRepository
    {

        public SecurityRepository(InvestmentContext context)
        {
            this.dbContext = context;
        }


        private InvestmentContext dbContext;



        public List<Security> LoadSecurities()
        {
            return this.dbContext.Securities
                .ToList();
        }

        public Security LoadSecurity(string symbol)
        {
            return this.dbContext.Securities
                .Where(s => s.Symbol == symbol)
                .FirstOrDefault();
        }
    }
}
