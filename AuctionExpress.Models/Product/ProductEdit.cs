﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Models
{
    public class ProductEdit
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? ProductCategoryId { get; set; }
       // public bool ProductIsActive { get; set; }
        public string ProductDescription { get; set; }
        public int ProductQuantity { get; set; }
        public DateTimeOffset ProductCloseTime { get; set; }
    }
}
