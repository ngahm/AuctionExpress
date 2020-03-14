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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy h:mm tt}")]
        public DateTimeOffset ProductCloseTime { get; set; }

        [Display(Name = "Product Start")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy h:mm tt}")]
        public DateTimeOffset ProductStartTime { get; set; }

        [Display(Name = "Minimum Selling Price")]
        public double MinimumSellingPrice { get; set; }
    }
}
