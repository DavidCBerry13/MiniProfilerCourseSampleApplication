using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentManager.Controllers
{

    [Authorize(Roles = "StockAnalyst,DevTeam")]
    public class MarketsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}