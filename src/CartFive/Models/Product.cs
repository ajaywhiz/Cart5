using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CartFive.Models
{
    public class Product
    {
        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [Display(Name="Product name")]
        [StringLength(40)]
        [Required(ErrorMessage="Product name is required.")]
        public string Name { get; set; }

        [Display(Name="Unit Price")]        
        [Required(ErrorMessage="Unit price is required.")]        
        public short Price { get; set; }

        [Display(Name="Product description")]
        [StringLength(500)]
        public string Description { get; set; }

        [Display(Name = "Product image Url")]
        [StringLength(255)]
        public string ImageUrl { get; set; }

    }
}