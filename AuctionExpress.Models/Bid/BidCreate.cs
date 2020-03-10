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
       
        public int ProductId { get; set; }
        //public string BidderId { get; set; }

        public double BidPrice { get; set; }
    }
}
