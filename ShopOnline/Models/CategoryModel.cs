using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace ShopOnline.Models
{
    public class CategoryModel
    {
        
        public Guid IdCategory { get; set; }
        [Display(Name = "Name category")]
        public string? Name { get; set; }
    }
}
