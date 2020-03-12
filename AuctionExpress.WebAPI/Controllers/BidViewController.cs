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
        // GET: BidViewByProductId
        public ActionResult GetBidsByProduct()
        {
            IEnumerable<BidListItem> bids = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Bid?productId=4");
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

        // GET: BidViewByProductId
        public ActionResult GetBidById(int id)
        {
            BidDetail bid = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                //HTTP GET
                var responseTask = client.GetAsync("bid/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<BidDetail>();
                    readTask.Wait();

                    bid = readTask.Result;
                }
                else //web api sent error response
                {
                    //log response status here..
                  //bid = //Enumerable.Empty<BidDetail>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

            }
            return View(bid);
        }
        public ActionResult CreateBid()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateBid(BidCreate bid)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<BidCreate>("bid", bid);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetBidsByProduct");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(bid);
        }
    }
}