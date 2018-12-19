using InvestmentManager.Core.DataAccess;
using InvestmentManager.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvestmentManager.DataAccess.EF.Repositories
{
    public class SecurityPriceRepository : ISecurityPriceRepository
    {

        public SecurityPriceRepository(InvestmentContext context)
        {
            this.dataContext = context;
        }

        private InvestmentContext dataContext;



        public List<SecurityPrice> LoadSecurityPrices(TradeDate date)
        {
            return this.dataContext.SecurityPrices
                .Include(price => price.Security)
                .Where(price => price.Date == date.Date)
                .ToList();
        }

        public List<SecurityPrice> LoadSecurityPrices(string symbol)
        {
            return this.dataContext.SecurityPrices
                .Include(price => price.Security)
                .Where(price => price.Symbol == symbol)
                    .ToList();
        }
    }
}
