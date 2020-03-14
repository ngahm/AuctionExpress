using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Models.Roles
{
    public class RoleUserView
    {
        [Display(Name = "User Id")]
        public string UserId { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "User is in this role?")]
        public bool IsSelected { get; set; }
    }
}
