using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Data
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int ProductQuantity { get; set; }
        public DateTimeOffset ProductStartTime { get; set; } = DateTimeOffset.Now;
        [Required]
        public DateTimeOffset ProductCloseTime { get; set; }
        public bool ProductIsActive { get; set; }

        [ForeignKey(nameof(ProductTransaction))]
        public int? ProductTransactionKey { get; set; }
        public virtual Transaction ProductTransaction { get; set; }

        [ForeignKey(nameof(ProductCategoryCombo))]
        public int? ProductCategoryId { get; set; }
        public virtual Category ProductCategoryCombo { get; set; }

        [ForeignKey(nameof(Seller))]
        [Required]
        public string ProductSeller { get; set; }
        public virtual ApplicationUser Seller { get; set; }

    }
}
