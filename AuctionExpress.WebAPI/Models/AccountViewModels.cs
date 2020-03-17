using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuctionExpress.WebAPI.Models
{
    // Models returned by AccountController actions.

    public class ExternalLoginViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class ManageInfoViewModel
    {
        public string LocalLoginProvider { get; set; }

        public string Email { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    public class UserInfoViewModel
    {
        public string Email { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }

    public class UserLoginInfoViewModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }

    public class UserListView
    {
        [Display(Name = "User Id")]
        public string UserId { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Email { get; set; }
        [Display(Name = "User Has Active Account?")]
        public bool IsActive { get; set; }
        [Display(Name = "Roles Assigned to User")]
        public IList<string> UserRoles { get; set; }
    }

    public class UserUpdateView
    {
        [Display(Name = "User Id")]
        public string UserId { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Email { get; set; }
        [Display(Name = "User Has Active Account?")]
        public bool IsActive { get; set; }
        [Display(Name = "Roles Assigned to User")]
        public List<RoleView> UpdateRoles { get; set; }

    }
    public class RoleView
    {
        [Display(Name = "Role Id")]
        public string RoleId { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        [Display(Name = "Is User in Role?")]
        public bool IsSelected { get; set; }
    }
}
