using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Models.Roles
{
    public class RoleDetail
    {
        [Display(Name = "Role Id")]
        public string Id { get; set; }
        [Display(Name = "Role Name")]
        public string Name { get; set; }
    }
}
