using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Data
{
    public class Bid
    {
        [Key]
        public int BidId { get; set; }

        [ForeignKey(nameof(Auction))]
        public int ProductId { get; set; }
        public virtual Product Auction { get; set; }

        [ForeignKey(nameof(Buyer))]
        public int BidderId { get; set; }
        public virtual User Buyer { get; set; }

        public DateTime TimeOfBid { get; set; }
        public double BidPrice { get; set; }
    }
}
