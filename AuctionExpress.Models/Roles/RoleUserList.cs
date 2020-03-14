using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Models.Roles
{
    public class RoleUserList
    {
        [Display(Name = "Role Id")]
        public string RoleId { get; set; }
        [Display(Name = "Users in this role.")]
        public List<RoleUserView> ListOfUsers { get; set; }
    }
}
