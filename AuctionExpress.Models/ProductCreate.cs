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
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int ProductCategoryId { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public int ProductQuantity { get; set; }
        public DateTime ProductStartTime { get; set; }
        [Required]
        public DateTime ProductCloseTime { get; set; }
    }
}
