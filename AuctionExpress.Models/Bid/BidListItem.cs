using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Models
{
    public class BidListItem
    {
        [Display(Name = "Bid Id")]
        public int BidId { get; set; }

        [Display(Name = "Product Id")]
        public int ProductId { get; set; }

        [Display(Name = "Bidder Id")]
        public string BidderId { get; set; }

        [Display(Name = "Bid Price")]
        public double BidPrice { get; set; }
    }
}
