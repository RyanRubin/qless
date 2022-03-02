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
    public class TransportLinesController : ControllerBase
    {
        private readonly TransportLineBiz biz;

        public TransportLinesController(QLessDbContext dbContext)
        {
            this.biz = new TransportLineBiz(dbContext);
        }

        // GET transportlines
        [HttpGet]
        public ResponseJson Get()
        {
            ResponseJson response = new ResponseJson();
            try
            {
                response.ReturnValue = biz.Get();
            }
            catch (BizException bizEx)
            {
                response.ErrorMessage = bizEx.Message;
            }
            return response;
        }
    }
}
