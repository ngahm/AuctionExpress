using AuctionExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace AuctionExpress.WebAPI.Controllers
{
    public class TransactionViewController : Controller
    {
        // GET: TransactionView
        public ActionResult GetTransaction()
        {
            IEnumerable<TransactionListItem> transactionViewer = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");

                var responseTask = client.GetAsync("transaction");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<TransactionListItem>>();
                    readTask.Wait();

                    transactionViewer = readTask.Result;
                }
                else      //web api sent error response
                {         //log response status here.
                    transactionViewer = Enumerable.Empty<TransactionListItem>();

                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                }

            }
            return View(transactionViewer);
        }


        public ActionResult PostTransaction()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostTransaction(TransactionCreate model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/transaction");

                //HTTP Post
                var postTask = client.PostAsJsonAsync<TransactionCreate>("transaction", model);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetTransaction");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administration.");

            return View(model);

        }


        public ActionResult GetTransactionById(int id)
        {
            TransactionDetail model = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");

                //HTTP Get
                var responseTask = client.GetAsync("transaction/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<TransactionDetail>();
                    readTask.Wait();

                    model = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error.Please contact administration.");
                }
            }

            return View(model);
        }

        public ActionResult PutTransaction(int id)
        {
            TransactionEdit model = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                //HTTP GET
                var responseTask = client.GetAsync("transaction/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<TransactionEdit>();
                    readTask.Wait();

                    model = readTask.Result;
                }
            }

            return View(model);
        }


        [HttpPost]
        public ActionResult PutTransaction(TransactionEdit model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");

                //HTTP 
                var putTask = client.PutAsJsonAsync<TransactionEdit>("transaction", model);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetTransaction");
                }
                return View(model);
            }
        }




        //public ActionResult Delete(int id)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("http://localhost:44320/api/");

        //        //HTTP Delete
        //        var deleteTask = client.DeleteAsync("transaction/" + id.ToString());
        //        deleteTask.Wait();

        //        var result = deleteTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("GetTransaction");
        //        }

        //    }
        //    return RedirectToAction("GetTransaction");
        //}

    }
}
