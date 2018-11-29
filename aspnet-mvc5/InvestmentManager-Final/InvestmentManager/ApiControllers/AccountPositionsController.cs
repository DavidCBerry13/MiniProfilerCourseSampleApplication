using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestmentManager.Core.DataAccess;
using System.Web.Http;

namespace InvestmentManager.ApiControllers
{
    [RoutePrefix("api/AccountPositions")]
    public class AccountPositionsController : ApiController
    {


        public AccountPositionsController(ITradeDateRepository tradeDateRepository, IAccountPositionsRepository positionsRepository)
        {
            this.tradeDateRepository = tradeDateRepository;
            this.positionsRepository = positionsRepository;
        }

        private ITradeDateRepository tradeDateRepository;
        private IAccountPositionsRepository positionsRepository;


        // GET: api/AccountPositions/5
        [HttpGet]
        [Route("{accountNumber}")]
        public IHttpActionResult Get(String accountNumber, DateTime date)
        {
            var tradeDates = this.tradeDateRepository.LoadTradeDates();
            var tradeDate = tradeDates.FirstOrDefault(td => td.Date == date.Date);
            if (tradeDate == null)
                return BadRequest($"The date {date:yyyy-mm-DD} is not a valid trade date");

            var positions = this.positionsRepository.LoadAccountPositions(accountNumber, tradeDate);

            return Ok(positions);
        }

    }
}
