using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ShopOnline.Models
{
    public class TVAmODEL
    {
        public Guid IdTva { get; set; }
        [Display(Name = "Cota TVA")]
      
        public decimal? TVA { get; set; }
        [ValidateNever]
        public Guid IdCategory { get; set; }
    }
}
