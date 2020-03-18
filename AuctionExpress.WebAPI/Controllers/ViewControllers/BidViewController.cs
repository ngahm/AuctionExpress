using AuctionExpress.Models;
using AuctionExpress.WebAPI.Models;
using Newtonsoft.Json;
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
        public ActionResult GetAllBids()
        {
            IEnumerable<BidListItem> bidViewer = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/");
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var responseTask = client.GetAsync("Bid/GetAllBids");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<BidListItem>>();
                    readTask.Wait();

                    bidViewer = readTask.Result;
                }
                else
                {
                    bidViewer = Enumerable.Empty<BidListItem>();

                    ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result);

                }

            }
            return View(bidViewer);
        }

        // GET: BidViewByProductId
        public ActionResult GetBidsByProduct(int id)
        {
            IEnumerable<BidListItem> bids = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var responseTask = client.GetAsync("Bid?productId=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<BidListItem>>();
                    readTask.Wait();

                    bids = readTask.Result;
                }
                else
                {
                    bids = Enumerable.Empty<BidListItem>();
                    ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result);
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
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var responseTask = client.GetAsync("bid/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<BidDetail>();
                    readTask.Wait();

                    bid = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result);
                }

            }
            return View(bid);
        }

        // GET: BidViewByUser
        public ActionResult GetBidsByUser()
        {
            IEnumerable<BidListItem> bidViewer = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var responseTask = client.GetAsync("Bid/");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<BidListItem>>();
                    readTask.Wait();

                    bidViewer = readTask.Result;
                }
                else
                {
                    bidViewer = Enumerable.Empty<BidListItem>();

                    ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result);
                }
            }
            return View(bidViewer);
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
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var postTask = client.PostAsJsonAsync<BidCreate>("bid", bid);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetBidsByUser");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result);
                }
            }

            return View(bid);
        }

        public ActionResult DeleteBid(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var deleteTask = client.DeleteAsync("bid/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("GetBidsByUser");
                }
                else { ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result); }
            }

            return RedirectToAction("GetBidsByUser");
        }

        #region Helper
        private HttpCookie CreateCookie(string token)
        {
            HttpCookie logInCookies = new HttpCookie("UserToken");
            logInCookies.Value = token;
            return logInCookies;
        }

        private string DeserializeToken()
        {
            var cookieValue = Request.Cookies["UserToken"];
            if (cookieValue != null)
            {
                var t = JsonConvert.DeserializeObject<Token>(cookieValue.Value);
                return t.access_token;
            }
            return null;
        }
        #endregion
    }
}