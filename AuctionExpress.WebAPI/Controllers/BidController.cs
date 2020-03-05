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

            if (!service.CreateBid(bid))
                return InternalServerError();

            return Ok();
        }

        //GET COMMENT ()
        public IHttpActionResult Get()
        {
            BidService bidService = CreateBidService();
            var notes = bidService.GetBid();
            return Ok(notes);
        }

        //GET COMMENT BY ID
        public IHttpActionResult Get(int id)
        {
            BidService bidService = CreateBidService();
            var note = bidService.GetBidById(id);
            return Ok(note);
        }


    }
}

