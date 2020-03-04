﻿using System;
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

        [ForeignKey(nameof(WinningBid))]
        public int BidId { get; set; }
        public virtual Bid WinningBid { get; set; }

        public bool IsPaid { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
