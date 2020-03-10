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
        [MaxLength(20)]
        [MinLength(2)]
        public string ProductName { get; set; }
        public int? ProductCategoryId { get; set; }
        [MaxLength(100)]
        [MinLength(2)]
        public string ProductDescription { get; set; }
        public int ProductQuantity { get; set; }
        public DateTimeOffset ProductStartTime { get; set; }
        public DateTimeOffset ProductCloseTime { get; set; }
    }
}
