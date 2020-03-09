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
    public class BidController : ApiController
    {
        private BidService CreateBidService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var bidService = new BidService(userId);
            return bidService;
        }



        //POST COMMENT ()

        public IHttpActionResult Post(BidCreate bid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateBidService();
            var entity = service.ValidateBid(bid);
            if (entity == null)
                return BadRequest("Invalid bid due to auciton being closed or bid being below product price.");

            if (!service.CreateBid(entity))
                return InternalServerError();

            return Ok();
        }

        //GET COMMENT ()
       public IHttpActionResult GetBid(int productId)
       {
           BidService bidService = CreateBidService();
           var bids = bidService.GetBid(productId);
           return Ok(bids);
        }

        //GET COMMENT BY ID
        public IHttpActionResult GetBidById(int id)
        {
            BidService bidService = CreateBidService();
            var bid = bidService.GetBidById(id);
            return Ok(bid);
        }


    }
}

