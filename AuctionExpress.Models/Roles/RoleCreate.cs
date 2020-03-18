using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Models
{
    public class RoleCreate
    {
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
