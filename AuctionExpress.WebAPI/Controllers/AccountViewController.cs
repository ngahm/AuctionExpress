using AuctionExpress.Models;
using AuctionExpress.Models.Roles;
using AuctionExpress.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Login(LoginBindingModel model, string returnUrl)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");

                var postTask = client.PostAsJsonAsync<LoginBindingModel>("Account/Login", model);
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


        public ActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRole(CreateRoleModel model)
        {
            using (var client = new HttpClient())
            {



                client.BaseAddress = new Uri("https://localhost:44320/api/");

                var postTask = client.PostAsJsonAsync<CreateRoleModel>("Account/AddRole", model);
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

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult GetRoles()
        {

            IEnumerable<RoleDetail> roleViewer = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");

                var responseTask = client.GetAsync("Account/GetRoles");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<RoleDetail>>();
                    readTask.Wait();

                    roleViewer = readTask.Result;
                }
                else      //web api sent error response
                {         //log response status here.
                    roleViewer = Enumerable.Empty<RoleDetail>();

                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                }

            }
            return View(roleViewer);
        }

        public ActionResult EditRole(string id)
        {
            EditRole role = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Account/GetRoleById?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<EditRole>();
                    readTask.Wait();

                    role = readTask.Result;
                }
            }

            return View(role);
        }


        [HttpPost]
        public ActionResult EditRole(EditRole role)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<EditRole>("Account/UpdateRole", role);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("GetRoles");
                }
            }
            return View(role);
        }

    //    public ActionResult EditUserRole(string id)
    //    {
    //        UserRoleList userRoleList = null; new UserRoleList();

    //        using (var client = new HttpClient())
    //        {
    //            client.BaseAddress = new Uri("https://localhost:44320/api/");
    //            //HTTP GET
    //            var responseTask = client.GetAsync("Account/GetRoleUsers?roleId=" + id);
    //            responseTask.Wait();

    //            var result = responseTask.Result;
    //            if (result.IsSuccessStatusCode)
    //            {
    //                var readTask = result.Content.ReadAsAsync<UserRoleList>();
    //                readTask.Wait();

    //                userRoleList = readTask.Result;
    //            }
    //            else      //web api sent error response
    //            {         //log response status here.

    //                userRoleList = new UserRoleList();
    //                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                    
    //            }
    //        }
    //        return View(userRoleList);
    //    }

    //    [HttpPost]
    //    public ActionResult EditUserRole(UserRoleList userRoleList)
    //    {
    //        using (var client = new HttpClient())
    //        {
    //            client.BaseAddress = new Uri("https://localhost:44320/api/");
    //            //HTTP POST
    //            var putTask = client.PutAsJsonAsync<UserRoleList>("Account/UpdateRoleUsers?roleId="+userRoleList.RoleId, userRoleList);
    //            putTask.Wait();

    //            var result = putTask.Result;
    //            if (result.IsSuccessStatusCode)
    //            {
    //                return RedirectToAction("GetRoles");
    //            }
    //        }
    //        return View(userRoleList);
    //    }
    }



}
