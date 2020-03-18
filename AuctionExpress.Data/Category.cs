using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Data
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [Description("Name of the Category, meant to be displayed to user.  Products can be assigned to a category for grouping purposes.")]
        /// <summary>
        /// Name of the Category, meant to be displayed to user.  Products can be assigned to a category for grouping purposes.
        /// </summary>
        public string CategoryName { get; set; }
    }
}
