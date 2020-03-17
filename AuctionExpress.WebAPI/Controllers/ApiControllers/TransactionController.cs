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
    [AllowAnonymous]
    [Authorize(Roles = "ActiveUser")]
    public class TransactionController : ApiController
    {

        private TransactionService CreateTransactionService()
        {
            Guid userId = new Guid();
            if (!User.Identity.IsAuthenticated)
            { userId = Guid.Parse("00000000-0000-0000-0000-000000000000"); }
            else
            { userId = Guid.Parse(User.Identity.GetUserId()); }
            var transactionService = new TransactionService(userId);
            return transactionService;
        }

        private ProductService CreateProductService()
        {
            var userId = Guid.Parse("1ae9afff-3752-45c4-a551-dc17f56033d8");
            //User.Identity.GetUserId());
            var productService = new ProductService(userId);
            return productService;
        }

        //CREATE Transaction
        /// <summary>
        /// Create a new transaction.
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public IHttpActionResult Post(TransactionCreate transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var prodService = CreateProductService();
            var prodDetail = prodService.ValidateAuctionStatus(transaction.ProductId);
            if (prodDetail == "Auction is closed")
            {
                var service = CreateTransactionService();

                if (!service.CreateTransaction(transaction))
                    return InternalServerError();

                return Ok("Transaction successfully created.");
            }
            return BadRequest("BadRequest" + prodDetail);
        }

        //GET POSTS
        /// <summary>
        /// Get all transactions where the user is the auction winner.
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            var service = CreateTransactionService();
            var transactions = service.GetTransactions();
            return Ok(transactions);
        }

        //GET POSTS BY IT
        /// <summary>
        /// Get a specific transaction by referencing the transaction id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            var service = CreateTransactionService();
            var transaction = service.GetTransactionById(id);
            return Ok(transaction);
        }

        //EDIT POST
        /// <summary>
        /// Update the status of a transaction.
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public IHttpActionResult Put(TransactionEdit transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateTransactionService();

            if (!service.UpdateTransaction(transaction))
                return InternalServerError();

            return Ok("Transaction succesfully updated.");
        }
        /// <summary>
        /// Delete a Transaction by transaction id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            var service = CreateTransactionService();

            if (!service.DeleteTransaction(id))
                return InternalServerError();

            return Ok();
        }
    }
}
