using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Models
{
    public class ProductDetail
    {
        public string ProductName { get; set; }
        public int? ProductCategoryId { get; set; }
        public string ProductDescription { get; set; }
        public int ProductQuantity { get; set; }
        public bool ProductIsActive { get; set; }
        public DateTimeOffset ProductStartTime { get; set; }
        public DateTimeOffset ProductCloseTime { get; set; }
        public string ProductSeller { get; set; }
        public double HighestBid { get; set; }
    }
}