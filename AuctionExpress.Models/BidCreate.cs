using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Models
{
    public class BidCreate
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int BidderId { get; set; }
        [Required]
        public double BidPrice { get; set; }
    }
}
