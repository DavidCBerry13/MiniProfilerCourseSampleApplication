using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestmentManager.Core.DataAccess;
using InvestmentManager.Core.Services;
using InvestmentManager.Models;
using System.Web;
using System.Web.Mvc;
using StackExchange.Profiling;

namespace InvestmentManager.Controllers
{
    public class InvestmentAccountsController : Controller
    {

        public InvestmentAccountsController(ITradeDateRepository tradeDateRepository, 
            IInvestmentAccountRepository accountRepository, RateOfReturnService rateOfReturnService)
        {
            this.tradeDateRepository = tradeDateRepository;
            this.accountRepository = accountRepository;
            this.rateOfReturnService = rateOfReturnService;
        }


        private ITradeDateRepository tradeDateRepository;
        private IInvestmentAccountRepository accountRepository;
        private RateOfReturnService rateOfReturnService;

        // GET: InvestmentAccounts
        public ActionResult Index()
        {
                var currentTradeDate = tradeDateRepository.GetLatestTradeDate();

                var accounts = this.accountRepository.LoadInvestmentAccounts(currentTradeDate).ToList();
                return View(accounts);                       
        }

        // GET: InvestmentAccounts/Details/XYZ1234567
        [HttpGet]
        [Route("InvestmentAccounts/Details/{accountNumber}")]
        public ActionResult Details(string accountNumber)
        {
            var currentTradeDate = tradeDateRepository.GetLatestTradeDate();

            var account = this.accountRepository.LoadInvestmentAccount(accountNumber, currentTradeDate);
            var performance = rateOfReturnService.CalculatePerformance(accountNumber);

            var viewModel = new InvestmentAccountDetailsModel()
            {
                TradeDate = currentTradeDate,
                InvestmentAccount = account,
                CurrentPerformance = performance.LastOrDefault()
            };

            return View(viewModel);
        }


       
    }
}