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
    public class BidController : ApiController
    {
        private BidService CreateBidService()
        {
            Guid userId = new Guid();
            if (!User.Identity.IsAuthenticated)
            { userId = Guid.Parse("00000000-0000-0000-0000-000000000000"); }
            else
            { userId = Guid.Parse(User.Identity.GetUserId()); }
            var bidService = new BidService(userId);
            return bidService;
        }
        private ProductService CreateProductService()
        {
            Guid userId = new Guid();
            if (!User.Identity.IsAuthenticated)
            { userId = Guid.Parse("00000000-0000-0000-0000-000000000000"); }
            else
            { userId = Guid.Parse(User.Identity.GetUserId()); }
            var productService = new ProductService(userId);
            return productService;
        }

        //POST Bid
        /// <summary>
        /// Place a new bid on an auction.
        /// </summary>
        /// <param name="bid"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "ActiveUser, Admin")]
        public IHttpActionResult Post(BidCreate bid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var prodService = CreateProductService();
            var prodDetail = prodService.ValidateBid(bid);

            if (prodDetail== "")
            {
                var service = CreateBidService();

                if (!service.CreateBid(bid))
                    return InternalServerError();

                return Ok("Bid successfully added.");
            }
            return BadRequest(prodDetail);
            
        }

        //GET All Bids
        /// <summary>
        /// Get all bids.
        /// </summary>
        /// <returns></returns>
        [Route("Bid/GetAllBids")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetAllBids()
        {
            BidService bidService = CreateBidService();
            var bids = bidService.GetAllBids();
            return Ok(bids);
        }

        //GET Bids By Product id
        /// <summary>
        /// Get all bids associated with a product.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [Authorize(Roles = "ActiveUser, Admin")]
        public IHttpActionResult GetBid(int productId)
       {
           BidService bidService = CreateBidService();
           var bids = bidService.GetBid(productId);
           return Ok(bids);
        }

        //GET Bid BY Bid ID
        /// <summary>
        /// Get a specific bid referenced by bid id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "ActiveUser, Admin")]
        public IHttpActionResult GetBidById(int id)
        {
            BidService bidService = CreateBidService();
            var bid = bidService.GetBidById(id);
            if (bid == null)
                return BadRequest("Bid does not exist.");
            return Ok(bid);
        }

        //GET Bid BY User
        /// <summary>
        /// Get all bids associated with a user id.
        /// </summary>
        /// <returns></returns>

       [Authorize(Roles ="ActiveUser, Admin")]
        public IHttpActionResult GetBidsByUser()
        {
            BidService bidService = CreateBidService();
            var bids = bidService.GetBidsByUser();
            return Ok(bids);
        }

        /// <summary>
        /// Delete Bid by bid Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles ="Admin")]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateBidService();
            string deleteResponse = service.DeleteBid(id);
            if (deleteResponse == "Bid successfully deleted")
                return Ok(deleteResponse);

            return InternalServerError();
        }
    }
}

