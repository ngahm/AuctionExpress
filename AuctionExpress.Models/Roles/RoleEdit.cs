using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Models.Roles
{
    public class RoleEdit
    {
        [Display(Name = "Role Id")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Role Name is required")]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        [Display(Name = "Users in this role.")]

        public List<string> Users { get; set; }
    }
}
