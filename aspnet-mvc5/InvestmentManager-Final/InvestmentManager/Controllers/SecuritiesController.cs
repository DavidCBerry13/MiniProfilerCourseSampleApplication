using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestmentManager.Core.DataAccess;
using System.Web.Mvc;
using InvestmentManager.Models;

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
                .OrderByDescending(d => d.Date);

            SecuritiesIndexModel model = new SecuritiesIndexModel();
            model.TradeDates = tradeDates.Select(x => new SelectListItem()
                { Text = x.Date.ToString("MM/dd/yyyy"), Value = x.Date.ToString("yyyy-MM-dd") }
                ).ToList();

            return View(model);
        }



    }
}