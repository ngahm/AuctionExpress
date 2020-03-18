using AuctionExpress.Models;
using AuctionExpress.WebAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
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

                    ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result);

                }

            }
            return View(productViewer);
        }

        public ActionResult GetAllProduct()
        {
            IEnumerable<ProductListItem> productViewer = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/");
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var responseTask = client.GetAsync("Product/GetAllAuctions");
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

                    ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result);

                }

            }
            return View(productViewer);
        }


        public ActionResult GetOpenProduct()
        {
            IEnumerable<ProductListItem> productViewer = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/");
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var responseTask = client.GetAsync("Product/GetOpenAuctions");
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

                    ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result);

                }

            }
            return View(productViewer);
        }

        public ActionResult GetOpenProdByCat(int Id)
        {
            IEnumerable<ProductListItem> productViewer = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/");
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var responseTask = client.GetAsync("Product/GetAuctionsByCat?Id=" + Id.ToString());
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

                    ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result);

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
                string token = DeserializeToken();
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                //HTTP Post
                var postTask = client.PostAsJsonAsync<ProductCreate>("product", product);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetProduct");
                }
            else { ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result); }
            }

            return View(product);

        }


        public ActionResult GetProductById(int id)
        {
            ProductDetail product = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                string token = DeserializeToken();
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
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

        public ActionResult PutProduct(int id)
        {
            ProductEdit product = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                //HTTP GET
                string token = DeserializeToken();
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var responseTask = client.GetAsync("product/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ProductEdit>();
                    readTask.Wait();

                    product = readTask.Result;
                }

                else { ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result); }

                return View(product);

            }

        }

        [HttpPost]

        public ActionResult PutProduct(ProductEdit product)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:44320/api/");
                string token = DeserializeToken();
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                //HTTP 
                var putTask = client.PutAsJsonAsync<ProductEdit>("product", product);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetProduct");
                }

                else { ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result); }
                return View(product);
            }
        }

        public ActionResult DeleteProduct(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("product/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetProduct");
                }
                else { ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result); }
            }
                return RedirectToAction("GetProduct");

        }


        #region Helper
        private string DeserializeToken()
        {
            var cookieValue = Request.Cookies["UserToken"];
            if (cookieValue!=null)
            {
                var t = JsonConvert.DeserializeObject<Token>(cookieValue.Value);
                return t.access_token;
            }
            return null;
        }
        #endregion

    }

}
