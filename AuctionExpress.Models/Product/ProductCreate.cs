using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Models
{
    public class ProductCreate
    {
        [MaxLength(20,ErrorMessage ="Product Name must be less than 20 characters.")]
        [MinLength(2, ErrorMessage = "Product Name must be more than 2 characters.")]
        public string ProductName { get; set; }
        public int? ProductCategoryId { get; set; }
        [MaxLength(100, ErrorMessage = "Product Description must be less than 100 characters.")]
        [MinLength(2, ErrorMessage = "Product Description must be more than 2 characters.")]
        public string ProductDescription { get; set; }
        [Range(0,int.MaxValue, ErrorMessage ="Quantity must be a number greater than zero.")]
        public int ProductQuantity { get; set; }
        [DataType(DataType.Date)]
        public DateTimeOffset ProductStartTime { get; set; }
        [DataType(DataType.Date)]
        public DateTimeOffset ProductCloseTime { get; set; }
    }
}
