using AuctionExpress.Data;
using AuctionExpress.Models;
using AuctionExpress.Models.Roles;
using AuctionExpress.WebAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace AuctionExpress.WebAPI.Controllers
{
    [RoutePrefix("api/Admin")]
    public class AdminController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        private ApplicationRoleManager _roleManager;



        public AdminController()
        {
        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? Request.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? Request.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set { _roleManager = value; }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return Request.GetOwinContext().Authentication;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        //[Authorize]
        [Route("AddRole")]
        public async Task<IHttpActionResult> CreateRole(RoleCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityRole identityRole = new IdentityRole
            {
                Name = model.RoleName
            };

            IdentityResult result = await RoleManager.CreateAsync(identityRole);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }
        [Route("GetRoles")]
        public IHttpActionResult GetRoles()
        {
            List<RoleDetail> roleDetails = new List<RoleDetail>();
            var roles = RoleManager.Roles;
            foreach (var item in roles)
            {
                new RoleDetail()
                {
                    Id = item.Id,
                    Name = item.Name
                };
                roleDetails.Add(new RoleDetail()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return Ok(roleDetails);
        }

        [Route("GetRoleById")]
        public IHttpActionResult GetRole(string id)
        {
            var role = RoleManager.FindById(id);
            if (role == null)
                return BadRequest("Role not found.");
            var model = new RoleEdit
            {
                Id = role.Id,
                RoleName = role.Name,
                Users = new List<string>()
            };
            var allUsers = new List<ApplicationUser>(UserManager.Users);
            foreach (var user in allUsers)
            {
                if (UserManager.IsInRole(user.Id, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return Ok(model);
        }

        [Route("UpdateRole")]
        [HttpPut]
        public IHttpActionResult UpdateRole(RoleEdit model)
        {
            var role = RoleManager.FindById(model.Id);
            if (role == null)
            {
                return BadRequest($"Role Id {model.Id} not found.");
            }

            role.Name = model.RoleName;
            IdentityResult result = RoleManager.Update(role);
            if (result.Succeeded)
                return Ok("Role successfully updated.");
            return InternalServerError();
        }

        [HttpGet]
        [Route("GetRoleUsers")]
        public IHttpActionResult EditUsersInRole(string roleId)
        {
            var role = RoleManager.FindById(roleId);
            if (role == null)
            {
                return BadRequest($"Role Id {roleId} not found.");
            }
            List<RoleUserView> model = new List<RoleUserView>();

            var allUsers = new List<ApplicationUser>(UserManager.Users);

            foreach (ApplicationUser user in allUsers)
            {
                RoleUserView userRoleView = new RoleUserView
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (UserManager.IsInRole(user.Id, role.Name))
                {
                    userRoleView.IsSelected = true;
                }
                model.Add(userRoleView);
            }

            RoleUserList userRoleList = new RoleUserList()
            {
                RoleId = roleId,
                ListOfUsers = model
            };

            return Ok(userRoleList);
        }
        [HttpPut]
        [Route("UpdateRoleUsers")]
        public IHttpActionResult EditUsersInRole(RoleUserList userRoleList)
        {
            var role = RoleManager.FindById(userRoleList.RoleId);
            if (role == null)
            {
                return BadRequest($"Role Id {userRoleList.RoleId} not found.");
            }
            foreach (var user in userRoleList.ListOfUsers)
            {
                IdentityResult result = null;
                var dbUser = UserManager.FindById(user.UserId);
                if (user.IsSelected && !UserManager.IsInRole(dbUser.Id, role.Name))
                {
                    result = UserManager.AddToRole(dbUser.Id, role.Name);
                }
                else if (!user.IsSelected && UserManager.IsInRole(dbUser.Id, role.Name))
                {
                    result = UserManager.RemoveFromRole(dbUser.Id, role.Name);
                }
                else { result = IdentityResult.Success; }

                if (!result.Succeeded)
                    return InternalServerError();
            }
            return Ok("Role successfully updated with users.");
        }

        [Route("DeleteRole")]
        public IHttpActionResult DeleteRole(string Id)
        {
            var role = RoleManager.FindById(Id);
            if(role==null)
                return BadRequest($"Role Id {Id} not found.");
            var result = RoleManager.Delete(role);
            if (!result.Succeeded)
                return InternalServerError();
            return Ok("Role successfully removed.");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        #endregion
    }
}
