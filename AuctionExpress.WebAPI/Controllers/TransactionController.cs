using AuctionExpress.Models;
using AuctionExpress.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AuctionExpress.WebAPI.Controllers
{
    public class TransactionController : ApiController
    {

        private TransactionService CreateTransactionService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var transactionService = new TransactionService(userId);
            return transactionService;
        }

        //CREATE Transaction
        public IHttpActionResult Post(TransactionCreate transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateTransactionService();

            if (!service.CreateTransaction(transaction))
                return InternalServerError();

            return Ok();
        }

        //GET POSTS
        public IHttpActionResult Get()
        {
            var service = CreateTransactionService();
            var transactions = service.GetTransactions();
            return Ok(transactions);
        }

        //GET POSTS BY IT
        public IHttpActionResult Get(int id)
        {
            var service = CreateTransactionService();
            var transaction = service.GetTransactionById(id);
            return Ok(transaction);
        }

        //EDIT POST
        public IHttpActionResult Put(TransactionEdit transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateTransactionService();

            if (!service.UpdateTransaction(transaction))
                return InternalServerError();

            return Ok();
        }
    }
}
}
