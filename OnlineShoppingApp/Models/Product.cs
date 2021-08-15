using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineShoppingApp.Models
{
    public class Product
    {

        public int Id { get; set; }
        [Required]
        [Display(Name = "Product Category")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Please enter product code")]
        [Display(Name = "Product Code")]
        public string ProductCode { get; set; }
        [Required]
        [Display(Name = "Product Title")]
        public string ProductTitle { get; set; }
        [Required]
        public string ProductImagePath { get; set; }
        [Required]
        [Display(Name = "Product Price")]
        public int ProductPrice { get; set; }
        [Required]
        [Display(Name = "Product Asking Price")]
        public int ProductAskPrice { get; set; }
        [Required]
        public string Status { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> ProductCategoryList { get; set; }
    }
}
