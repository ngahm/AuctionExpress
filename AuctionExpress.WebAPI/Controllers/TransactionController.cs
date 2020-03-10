﻿using AuctionExpress.Models;
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
        /// <summary>
        /// Create a new transaction.
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public IHttpActionResult Post(TransactionCreate transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateTransactionService();

            if (!service.CreateTransaction(transaction))
                return InternalServerError();

            return Ok("Transaction successfully created.");
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
        /// get a specific transaction by referencing the transaction id.
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
    }
}
