using AuctionExpress.Models;
using AuctionExpress.Models.Roles;
using AuctionExpress.WebAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AuctionExpress.WebAPI.Controllers
{
    public class AccountViewController : Controller
    {
        // POST: Register User
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RegisterBindingModel model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");

                var postTask = client.PostAsJsonAsync<RegisterBindingModel>("Account/Register", model);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login", "AccountView");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error.  Please contact administrator.");

            return View(model);


        }


        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginBindingModel model, string returnUrl)
        {
                var pairs = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>( "grant_type", "password" ),
                        new KeyValuePair<string, string>( "username", model.UserName ),
                        new KeyValuePair<string, string> ( "Password", model.Password )
                    };
                var content = new FormUrlEncodedContent(pairs);
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/");
                var response = client.PostAsync("token", content).Result;
                var token= response.Content.ReadAsStringAsync().Result;
                Response.Cookies.Add(CreateCookie(token));
                //Response.Flush();
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetOpenProduct", "ProductView");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login. Please try again");

                return View();
            }
            //Response.Cookies.Append("Token", token);
        }

        public ActionResult LogOff()
        {
            if (Request.Cookies["UserToken"] != null)
            {
                Response.Cookies["UserToken"].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Login","AccountView");
        }

        public ActionResult DeactivateUser(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult DeactivateUser(LoginBindingModel model)
        {
            using (var client = new HttpClient())
            {
                string token = DeserializeToken();
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                //HTTP Post
                var postTask = client.PostAsJsonAsync<LoginBindingModel>("Account/DeactivateUser", model);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login", "AccountView");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administration.");

            return View(model);
        }
        #region Helper
        private HttpCookie CreateCookie(string token)
        {
            HttpCookie logInCookies = new HttpCookie("UserToken");
            logInCookies.Value = token;
            //StudentCookies.Expires = DateTime.Now.AddHours(1);
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




