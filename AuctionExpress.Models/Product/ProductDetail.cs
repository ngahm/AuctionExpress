using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Models
{
    public class ProductDetail
    {
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Product Category Id")]
        public int? ProductCategoryId { get; set; }

        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }

        [Display(Name = "Product Quantity")]
        public int ProductQuantity { get; set; }

        [Display(Name = "Product Is Active")]
        public bool ProductIsActive { get; set; }

        [Display(Name = "Product Start")]
        public DateTimeOffset ProductStartTime { get; set; }

        [Display(Name = "Product Close")]
        public DateTimeOffset ProductCloseTime { get; set; }

        [Display(Name = "Product Seller")]
        public string ProductSeller { get; set; }

        [Display(Name = "Highest Bid")]
        public double HighestBid { get; set; }
    }
}