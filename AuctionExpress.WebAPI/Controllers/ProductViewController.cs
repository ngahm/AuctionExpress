using AuctionExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace AuctionExpress.WebAPI.Controllers
{
    public class ProductViewController : Controller
    {
        // GET: MVC
        public ActionResult GetProduct()
        {
            IEnumerable<ProductListItem> productViewer = null;

            using (var client = new HttpClient())
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


        public ActionResult PostProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostProduct(ProductCreate product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44320/api/product");

                //HTTP Post
                var postTask = client.PostAsJsonAsync<ProductCreate>("product", product);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetProduct");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administration.");

            return View(product);

        }


        public ActionResult GetProductById(int id)
        {
            ProductDetail product = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44320/api/");

                //HTTP Get
                var responseTask = client.GetAsync("product/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ProductDetail>();
                    readTask.Wait();

                    product = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error.Please contact administration.");
                }
            }

            return View(product);
        }

        [HttpPost]
        public ActionResult PutProduct(ProductEdit product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44320/api/product");

                //HTTP 
                var putTask = client.PutAsJsonAsync<ProductEdit>("product", product);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetProduct");
                }
                return View(product);
            }
        }


        //public ActionResult Delete(int id)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("http://localhost:44320/api/");

        //        //HTTP Delete
        //        var deleteTask = ClientCertificateOption.DeleteAsync("student/" + id.ToString());
        //        deleteTask.Wait();

        //        var result = deleteTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("GetProduct");
        //        }

        //    }
        //}

    }   

}
