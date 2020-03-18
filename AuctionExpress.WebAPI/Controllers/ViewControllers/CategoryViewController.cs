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
    public class CategoryViewController : Controller
    {
        // GET: CategoryView
        public ActionResult GetCategories()
        {
            IEnumerable<CategoryListItem> categories = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var responseTask = client.GetAsync("category");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<CategoryListItem>>();
                    readTask.Wait();

                    categories = readTask.Result;
                }
                else
                {
                    categories = Enumerable.Empty<CategoryListItem>();

                    ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result);
                }
            }
            return View(categories);
        }

        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategory(CategoryCreate category)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/category");
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var postTask = client.PostAsJsonAsync<CategoryCreate>("category", category);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetCategories");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result);
                }
            }
            return View(category);
        }

        public ActionResult EditCategory(int id)
        {
            CategoryEdit category = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var responseTask = client.GetAsync("category/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CategoryEdit>();
                    readTask.Wait();

                    category = readTask.Result;
                }
                else { ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result); }
            }

            return View(category);
        }


        [HttpPost]
        public ActionResult EditCategory(CategoryEdit category)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var putTask = client.PutAsJsonAsync<CategoryEdit>("category", category);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("GetCategories");
                }
                else { ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result); }
            }
            return View(category);
        }

        public ActionResult GetCategoryById(int id)
        {
            CategoryDetail category = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var responseTask = client.GetAsync("category/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CategoryDetail>();
                    readTask.Wait();

                    category = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result);
                }

            }
            return View(category);
        }

        public ActionResult DeleteCategory(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var deleteTask = client.DeleteAsync("category/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("GetCategories");
                }
                else { ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result); }
            }

            return RedirectToAction("GetCategories");
        }
        #region Helper
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