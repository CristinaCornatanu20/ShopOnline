using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ShopOnline.Models
{
    public class ProductModel
    {
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid IdProduct { get; set; }
    [Display(Name = "Name product")]
    [Required(ErrorMessage = "Please enter product name")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Please enter product description")]
    public string? Description { get; set; }
    [Required(ErrorMessage = "Please select product category")]
    
    public Guid IdCategory { get; set; }
    [Required(ErrorMessage = "Please enter product price")]
    public decimal? Price { get; set; }
    [Display(Name = "Stock Quantity")]
    [Required(ErrorMessage = "Please enter product stock quantity")]
    public int? StockQuantity { get; set; }
    [Display(Name = "Image")]
    [ValidateNever]
     public string? Image { get; set; }

        public virtual CategoryModel IdCategoryNavigation { get; set; } = null!;
    }
}

