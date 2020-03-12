using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Models
{
    public class ProductEdit
    {
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }

        [Display(Name = "Product Name")]
        [MaxLength(20, ErrorMessage = "Product Name must be less than 20 characters.")]
        [MinLength(2, ErrorMessage = "Product Name must be more than 2 characters.")]
        public string ProductName { get; set; }

        [Display(Name = "Product Category Id")]
        public int? ProductCategoryId { get; set; }
        // public bool ProductIsActive { get; set; }

        [Display(Name = "Product Description")]
        [MaxLength(100, ErrorMessage = "Product Description must be less than 100 characters.")]
        [MinLength(2, ErrorMessage = "Product Description must be more than 2 characters.")]
        public string ProductDescription { get; set; }

        [Display(Name = "Product Quantity")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a number greater than zero.")]
        public int ProductQuantity { get; set; }

        [Display(Name = "Transaction Id")]
        [DataType(DataType.Date)]
        public DateTimeOffset ProductCloseTime { get; set; }
    }
}
