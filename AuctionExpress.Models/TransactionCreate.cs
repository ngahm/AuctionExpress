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
        
        public int ProductId { get; set; }
        public bool IsPaid { get; set; }
    }
}
