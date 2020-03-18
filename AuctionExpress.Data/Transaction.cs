using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Data
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [ForeignKey(nameof(TransactionProduct))]
        public int ProductId { get; set; }
        public virtual Product TransactionProduct { get; set; }

        public Bid WinningBid
        {
            get
            {
                Bid _winningBid = TransactionProduct.ProductBids
                    .Single(x => x.BidPrice == TransactionProduct.HighestBid);

                return _winningBid;
            }
        }

        [Required]
        public bool IsPaid { get; set; }
        public DateTimeOffset? PaymentDate { get; set; }
    }
}