using AuctionExpress.Models;
using AuctionExpress.Models.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace AuctionExpress.WebAPI.Controllers
{
    public class AdminViewController : Controller
    {
        // GET: AdminView
       
        public ActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRole(RoleCreate model)
        {
            using (var client = new HttpClient())
            {



                client.BaseAddress = new Uri("https://localhost:44320/api/");

                var postTask = client.PostAsJsonAsync<RoleCreate>("Admin/AddRole", model);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetRoles");
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

                var responseTask = client.GetAsync("Admin/GetRoles");
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
            RoleEdit role = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Admin/GetRoleById?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<RoleEdit>();
                    readTask.Wait();

                    role = readTask.Result;
                }
            }

            return View(role);
        }




        [HttpPost]
        public ActionResult EditRole(RoleEdit role)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<RoleEdit>("Admin/UpdateRole", role);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("GetRoles");
                }
            }
            return View(role);
        }


        public ActionResult EditUserRole(string id)
        {
            RoleUserList userRoleList = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Admin/GetRoleUsers?roleId=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readtask = result.Content.ReadAsAsync<RoleUserList>();
                    readtask.Wait();

                    userRoleList = readtask.Result;
                }
            }

            return View(userRoleList);
        }

        [HttpPost]
        public ActionResult EditUserRole(RoleUserList userRoleList)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<RoleUserList>("Admin/UpdateRoleUsers", userRoleList);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("GetRoles");
                }
            }
            return View(userRoleList);
        }

        public ActionResult DeleteRole(string Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("Admin/DeleteRole?Id=" + Id);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetRoles");
                }
            }

            return RedirectToAction("GetRoles");


        }

  
    }
}