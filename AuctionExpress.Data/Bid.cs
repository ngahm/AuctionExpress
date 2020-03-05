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
        [Required]
        public int ProductId { get; set; }
        public virtual Product Auction { get; set; }

        [ForeignKey(nameof(Buyer))]
        [Required]
        public string BidderId { get; set; }
        public virtual ApplicationUser Buyer { get; set; }

        public DateTimeOffset TimeOfBid { get; set; } = DateTimeOffset.Now;
        [Required]
        [DataType(DataType.Currency)]
        public double BidPrice { get; set; }

    }
}
