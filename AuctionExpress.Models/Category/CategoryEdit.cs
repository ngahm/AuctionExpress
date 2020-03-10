﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Models
{
    public class CategoryEdit
    {
        public int CategoryId { get; set; }
        [MaxLength(20, ErrorMessage = "Category Name must be less than 20 characters.")]
        [MinLength(2, ErrorMessage = "Category Name must be more than 2 characters.")]
        public string CategoryName { get; set; }
    }
}