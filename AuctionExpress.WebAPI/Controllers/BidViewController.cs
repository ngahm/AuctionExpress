using AuctionExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace AuctionExpress.WebAPI.Controllers
{
    public class BidViewController : Controller
    {
        // GET: BidView
        public ActionResult GetBidsByProduct()
        {
            IEnumerable<BidListItem> bids = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Bid?productId=2");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<BidListItem>>();
                    readTask.Wait();

                    bids = readTask.Result;
                }
                else //web api sent error response
                {
                    //log response status here..
                    bids = Enumerable.Empty<BidListItem>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

            }
            return View(bids);
        }
    }
}