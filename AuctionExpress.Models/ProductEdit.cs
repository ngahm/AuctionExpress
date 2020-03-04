using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Models
{
    public class ProductEdit
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductCategoryId { get; set; }
        public bool ProductIsActive { get; set; }
        public string ProductDescription { get; set; }
        public int ProductQuantity { get; set; }
        public DateTime ProductCloseTime { get; set; }
    }
}
