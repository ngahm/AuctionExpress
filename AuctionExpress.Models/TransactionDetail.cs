﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Models
{
    public class TransactionDetail
    {
        public int TransactionId { get; set; }
        public int BidId { get; set; }
        public string ProductName { get; set; }
        public string UserName { get; set; }
        public double BidPrice { get; set; }
        public bool IsPaid { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
