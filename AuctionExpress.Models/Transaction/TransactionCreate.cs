using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Models
{
    public class TransactionCreate
    {
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }

        [Display(Name = "Is Paid")]
        public bool IsPaid { get; set; }
    }
}