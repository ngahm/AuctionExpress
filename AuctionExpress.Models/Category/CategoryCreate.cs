﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Models
{
    public class CategoryCreate
    {
        [Display(Name = "Category Name")]
        [MaxLength(20, ErrorMessage = "Category Name must be less than 20 characters.")]
        [MinLength(2, ErrorMessage = "Category Name must be more than 2 characters.")]
        [Description("Name of the Category, meant to be displayed to user.  Products can be assigned to a category for grouping purposes.")]
        /// <summary>
        /// Name of the Category, meant to be displayed to user.  Products can be assigned to a category for grouping purposes.
        /// </summary>
        public string CategoryName { get; set; }
    }
}
