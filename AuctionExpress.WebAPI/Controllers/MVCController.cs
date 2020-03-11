using AuctionExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace AuctionExpress.WebAPI.Controllers
{
    public class MVCController : Controller
    {
        // GET: MVC
        public ActionResult Index()
        {
            IEnumerable<ProductListItem> productViewer = null; 

            using (var client=new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");

                var responseTask = client.GetAsync("product");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProductListItem>>();
                    readTask.Wait();

                    productViewer = readTask.Result;
                }
                else      //web api sent error response
                {         //log response status here.
                    productViewer = Enumerable.Empty<ProductListItem>();

                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                }
                
            }
            return View(productViewer);
        }
    }
}