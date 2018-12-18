using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockIndexWebService.Data;
using StockIndexWebService.ViewModel;

namespace StockIndexWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockIndexesController : ControllerBase
    {

        public StockIndexesController(StockIndexDbContext dbContext)
        {
            this.dataContext = dbContext;
        }

        private StockIndexDbContext dataContext;

        // GET api/values
        [HttpGet]
        public ActionResult<List<StockIndexModel>> Get()
        {
            // Introduce some delay to simulate the delay you have in calling a service over the wire
            Thread.Sleep(new Random().Next(250, 750));

            var stockIndexes = this.dataContext.StockIndexes;

            var models = stockIndexes.Select(ix => new StockIndexModel()
            {
                Code = ix.Code,
                Name = ix.Name,
                ShortDisplayName = ix.ShortDisplayName
            }).ToList();

            return Ok(models);
        }

     
    }
}
