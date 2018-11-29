using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InvestmentManager.Controllers
{
    public class MarketsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}