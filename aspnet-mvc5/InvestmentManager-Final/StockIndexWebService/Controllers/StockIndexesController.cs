using StockIndexWebService.Data;
using StockIndexWebService.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace StockIndexWebService.Controllers
{
    public class StockIndexesController : ApiController
    {
        public StockIndexesController()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["StockIndexDatabase"].ConnectionString;
            this.dataContext = new StockIndexDbContext(connectionString);
        }

        private StockIndexDbContext dataContext;

        // GET api/values
        [HttpGet]
        [Route("api/StockIndexes")]
        public IHttpActionResult Get()
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