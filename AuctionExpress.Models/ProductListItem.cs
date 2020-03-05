using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Models
{
    public class ProductListItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int ProductQuantity { get; set; }
        public bool ProductIsActive { get; set; }
        public DateTimeOffset ProductCloseTime { get; set; }
    }
}
