using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Data
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public Guid UserGuid { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        public string BusinessName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
