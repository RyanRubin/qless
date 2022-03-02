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
    public class TransportCardsController : ControllerBase
    {
        private readonly TransportCardBiz biz;

        public TransportCardsController(QLessDbContext dbContext)
        {
            this.biz = new TransportCardBiz(dbContext);
        }

        // POST transportcards
        [HttpPost]
        public ResponseJson Post()
        {
            ResponseJson response = new ResponseJson();
            try
            {
                TransportCard transportCard = biz.BuyNew();
                response.ReturnValue = new { transportCard.Id, transportCard.Balance };
            }
            catch (BizException bizEx)
            {
                response.ErrorMessage = bizEx.Message;
            }
            return response;
        }

        // GET transportcards/{id}/balance
        [HttpGet("{id}/balance")]
        public ResponseJson GetBalance(string id)
        {
            ResponseJson response = new ResponseJson();
            try
            {
                response.ReturnValue = biz.CheckBalance(id);
            }
            catch (BizException bizEx)
            {
                response.ErrorMessage = bizEx.Message;
            }
            return response;
        }

        // POST transportcards/{id}/reload/{loadAmount}
        [HttpPost("{id}/reload/{loadAmount}")]
        public ResponseJson PostReload(string id, decimal loadAmount)
        {
            ResponseJson response = new ResponseJson();
            try
            {
                response.ReturnValue = biz.ReloadCard(id, loadAmount);
            }
            catch (BizException bizEx)
            {
                response.ErrorMessage = bizEx.Message;
            }
            return response;
        }

        // POST transportcards/{id}/regdiscount
        [HttpPost("{id}/regdiscount")]
        public ResponseJson PostRegDiscount(string id, string seniorCitizenControlNumber, string pwdIdNumber)
        {
            ResponseJson response = new ResponseJson();
            try
            {
                biz.DiscountedCardRegistration(id, seniorCitizenControlNumber, pwdIdNumber);
            }
            catch (BizException bizEx)
            {
                response.ErrorMessage = bizEx.Message;
            }
            return response;
        }

        // POST transportcards/{id}/tapin/{transportLineId}/{fromStationId}
        [HttpPost("{id}/tapin/{transportLineId}/{fromStationId}")]
        public ResponseJson PostTapIn(string id, string transportLineId, string fromStationId)
        {
            ResponseJson response = new ResponseJson();
            try
            {
                response.ReturnValue = biz.TapIn(id, transportLineId, fromStationId);
            }
            catch (BizException bizEx)
            {
                response.ErrorMessage = bizEx.Message;
            }
            return response;
        }

        // POST transportcards/{id}/tapout/{transportLineId}/{toStationId}
        [HttpPost("{id}/tapout/{transportLineId}/{toStationId}")]
        public ResponseJson PostTapOut(string id, string transportLineId, string toStationId)
        {
            ResponseJson response = new ResponseJson();
            try
            {
                response.ReturnValue = biz.TapOut(id, transportLineId, toStationId);
            }
            catch (BizException bizEx)
            {
                response.ErrorMessage = bizEx.Message;
            }
            return response;
        }
    }
}
