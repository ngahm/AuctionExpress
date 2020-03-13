using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Models
{
    public class TransactionListItem
    {
        [Display(Name = "Transaction Id")]
        public int TransactionId { get; set; }
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }
        [Display(Name = "Seller Id")]
        public string SellerId { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Is Paid")]
        public bool IsPaid { get; set; }
        [Display(Name = "Payment Date")]
        public DateTimeOffset? PaymentDate { get; set; }


    }
}