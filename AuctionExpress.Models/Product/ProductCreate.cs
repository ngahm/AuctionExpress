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
        [Display(Name = "Product Name")]
        [MaxLength(20,ErrorMessage ="Product Name must be less than 20 characters.")]
        [MinLength(2, ErrorMessage = "Product Name must be more than 2 characters.")]
        public string ProductName { get; set; }

        [Display(Name = "Product Category Id")]
        public int? ProductCategoryId { get; set; }

        [Display(Name = "Product Description")]
        [MaxLength(100, ErrorMessage = "Product Description must be less than 100 characters.")]
        [MinLength(2, ErrorMessage = "Product Description must be more than 2 characters.")]
        public string ProductDescription { get; set; }

        [Display(Name = "Product Quantity")]
        [Range(0,int.MaxValue, ErrorMessage ="Quantity must be a number greater than zero.")]
        public int ProductQuantity { get; set; }

        [Display(Name = "Product Start")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy h:mm}")]
        public DateTimeOffset ProductStartTime { get; set; }

        [Display(Name = "Product Close")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy h:mm tt}")]
        public DateTimeOffset ProductCloseTime { get; set; }

        [Display(Name = "Minimum Selling Price")]
        [Range(0,double.MaxValue, ErrorMessage ="Price must be a number greater than zero.")]
        public double MinimumSellingPrice { get; set; }
    }
}
