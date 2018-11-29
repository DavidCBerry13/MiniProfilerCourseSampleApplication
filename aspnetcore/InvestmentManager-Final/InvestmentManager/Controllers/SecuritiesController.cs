using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestmentManager.Core.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentManager.Controllers
{
    public class SecuritiesController : Controller
    {

        public SecuritiesController(ITradeDateRepository tradeDateRepository)
        {
            this.tradeDateRepository = tradeDateRepository;           
        }

        public ITradeDateRepository tradeDateRepository;



        // GET: Securities
        public ActionResult Index()
        {
            var tradeDates = tradeDateRepository.LoadTradeDates()
                .OrderBy(d => d.Date);

            return View(tradeDates);
        }

        // GET: Securities/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


    }
}