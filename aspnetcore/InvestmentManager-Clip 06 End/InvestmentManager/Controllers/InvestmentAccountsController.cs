using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestmentManager.Core.DataAccess;
using InvestmentManager.Core.Services;
using InvestmentManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [Route("[controller]/[action]/{accountNumber}")]
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

        // GET: InvestmentAccounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InvestmentAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InvestmentAccounts/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InvestmentAccounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InvestmentAccounts/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InvestmentAccounts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}