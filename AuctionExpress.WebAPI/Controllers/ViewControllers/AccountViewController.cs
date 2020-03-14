using AuctionExpress.Models;
using AuctionExpress.Models.Roles;
using AuctionExpress.WebAPI.Models;
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
                    return RedirectToAction("Index");
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
        public string Login(LoginBindingModel model, string returnUrl)
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
                return response.Content.ReadAsStringAsync().Result;
                
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");

                var postTask = client.DeleteAsync("Account/LogOff");
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error.  Please contact administrator.");

            return View();

        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult GetToken()
        //{
        //    string GetToken(string url, string userName, string password)
        //    {
        //        var pairs = new List<KeyValuePair<string, string>>
        //            {
        //                new KeyValuePair<string, string>( "grant_type", "password" ),
        //                new KeyValuePair<string, string>( "username", userName ),
        //                new KeyValuePair<string, string> ( "Password", password )
        //            };
        //        var content = new FormUrlEncodedContent(pairs);
        //        ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        //        using (var client = new HttpClient())
        //        {
        //            var response = client.PostAsync(url + "Token", content).Result;
        //            return response.Content.ReadAsStringAsync().Result;
        //        }
        //    }
        //}




    }

}




