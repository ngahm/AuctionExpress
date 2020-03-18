using AuctionExpress.Models;
using AuctionExpress.Models.Roles;
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
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var postTask = client.PostAsJsonAsync<RoleCreate>("Admin/AddRole", model);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetRoles");
                }
                ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult GetRoles()
        {

            IEnumerable<RoleDetail> roleViewer = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var responseTask = client.GetAsync("Admin/GetRoles");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<RoleDetail>>();
                    readTask.Wait();

                    roleViewer = readTask.Result;
                }
                else
                {
                    roleViewer = Enumerable.Empty<RoleDetail>();

                    ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result);
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
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var responseTask = client.GetAsync("Admin/GetRoleById?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<RoleEdit>();
                    readTask.Wait();
                    role = readTask.Result;
                }
                else { ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result); }
            }

            return View(role);
        }

        [HttpPost]
        public ActionResult EditRole(RoleEdit role)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var putTask = client.PutAsJsonAsync<RoleEdit>("Admin/UpdateRole", role);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetRoles");
                }
                else { ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result); }
            }
            return View(role);
        }

        public ActionResult EditUserRole(string id)
        {
            RoleUserList userRoleList = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var responseTask = client.GetAsync("Admin/GetRoleUsers?roleId=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readtask = result.Content.ReadAsAsync<RoleUserList>();
                    readtask.Wait();
                    userRoleList = readtask.Result;
                }
                else { ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result); }
            }
            return View(userRoleList);
        }

        [HttpPost]
        public ActionResult EditUserRole(RoleUserList userRoleList)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var putTask = client.PutAsJsonAsync<RoleUserList>("Admin/UpdateRoleUsers", userRoleList);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("GetRoles");
                }
                else { ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result); }
            }
            return View(userRoleList);
        }

        public ActionResult DeleteRole(string Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var deleteTask = client.DeleteAsync("Admin/DeleteRole?Id=" + Id);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetRoles");
                }
                else { ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result); }
            }

            return RedirectToAction("GetRoles");
        }

        [HttpGet]
        public ActionResult GetUsers()
        {

            IEnumerable<UserListView> userViewer = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var responseTask = client.GetAsync("Admin/GetAllUsers");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<UserListView>>();
                    readTask.Wait();

                    userViewer = readTask.Result;
                }
                else
                {
                    userViewer = Enumerable.Empty<UserListView>();

                    ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result);
                }
            }
            return View(userViewer);
        }

        public ActionResult UpdateUser(string id)
        {
            UserUpdateView user = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var responseTask = client.GetAsync("Admin/GetUser?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<UserUpdateView>();
                    readTask.Wait();
                    user = readTask.Result;
                }
                else { ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result); }
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult UpdateUser(UserUpdateView user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44320/api/");
                string token = DeserializeToken();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var putTask = client.PutAsJsonAsync<UserUpdateView>("Admin/UpdateUser", user);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetUsers");
                }
                else { ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result); }
            }
            return View(user);
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