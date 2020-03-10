﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Models
{
    public class TransactionListItem
    {
        public int TransactionId { get; set; }
        public int ProductId { get; set; }
        public string SellerId { get; set; }
        public string ProductName { get; set; }
        public bool IsPaid { get; set; }
        public DateTimeOffset? PaymentDate { get; set; }


    }
}