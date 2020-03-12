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
            var userId = Guid.Parse("137ae0c4-7144-445d-b6c0-2918a3dd5907");//User.Identity.GetUserId());
            var bidService = new BidService(userId);
            return bidService;
        }
        private ProductService CreateProductService()
        {
            var userId = Guid.Parse("1ae9afff-3752-45c4-a551-dc17f56033d8");//User.Identity.GetUserId());
            var productService = new ProductService(userId);
            return productService;
        }



        //POST Bid
        /// <summary>
        /// Place a new bid on an auction.
        /// </summary>
        /// <param name="bid"></param>
        /// <returns></returns>
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

        //GET Bid
        /// <summary>
        /// Get all bids associated with a product.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
       public IHttpActionResult GetBid(int productId)
       {
           BidService bidService = CreateBidService();
           var bids = bidService.GetBid(productId);
           return Ok(bids);
        }

        //GET Bid BY ID
        /// <summary>
        /// Get a specific bid referenced by bid id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult GetBidById(int id)
        {
            BidService bidService = CreateBidService();
            var bid = bidService.GetBidById(id);
            if (bid == null)
                return BadRequest("Bid does not exist.");
            return Ok(bid);
        }


    }
}

