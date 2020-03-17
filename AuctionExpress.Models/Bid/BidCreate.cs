using AuctionExpress.Data;
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
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }
        //public string BidderId { get; set; }

        [Display(Name = "Bid Price")]
        public double BidPrice { get; set; }
    }
}
