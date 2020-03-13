using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Models
{
    public class ProductListItem
    {
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Display(Name = "Product Quantity")]
        public int ProductQuantity { get; set; }

        [Display(Name = "Product Is Active")]
        public bool ProductIsActive
        {
            get
            {
                if (DateTimeOffset.Now < ProductStartTime || DateTimeOffset.Now > ProductCloseTime)
                {
                    return false;
                }
                else
                    return true;
            }
        }

        [Display(Name = "Product Close")]
        public DateTimeOffset ProductCloseTime { get; set; }

        [Display(Name = "Product Start")]
        public DateTimeOffset ProductStartTime { get; set; }
    }
}
