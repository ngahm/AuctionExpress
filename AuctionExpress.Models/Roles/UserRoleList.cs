using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Models.Roles
{
    public class UserRoleList
    {
        public string RoleId { get; set; }
        public List<UserRoleView> ListOfUsers { get; set; }
    }
}
