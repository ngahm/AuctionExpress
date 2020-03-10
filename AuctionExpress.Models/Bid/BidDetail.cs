﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Models
{
    public class BidDetail
    {
        public int BidId { get; set; }
        public int ProductId { get; set; }
        public string BidderId { get; set; }
        public DateTimeOffset TimeOfBid { get; set; }
        public double BidPrice { get; set; }

    }
}