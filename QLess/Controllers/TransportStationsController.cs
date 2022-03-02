using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QLess.Models;
using QLess.Db;
using QLess.Biz;

namespace QLess.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransportStationsController : ControllerBase
    {
        private readonly TransportStationBiz biz;

        public TransportStationsController(QLessDbContext dbContext)
        {
            this.biz = new TransportStationBiz(dbContext);
        }

        // GET transportstations/{transportLineId}/line
        [HttpGet("{transportLineId}/line")]
        public ResponseJson GetBalance(string transportLineId)
        {
            ResponseJson response = new ResponseJson();
            try
            {
                response.ReturnValue = biz.GetFromTransportLineId(transportLineId);
            }
            catch (BizException bizEx)
            {
                response.ErrorMessage = bizEx.Message;
            }
            return response;
        }
    }
}
