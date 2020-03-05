using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Data
{
    public class User
    {
        [Key]
        
        public int UserId { get; set; }
        
        [ForeignKey(nameof(ApplicationUser))]
        public Guid ForeignUserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public Guid UserGuid { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string BusinessName { get; set; }



    }
}
