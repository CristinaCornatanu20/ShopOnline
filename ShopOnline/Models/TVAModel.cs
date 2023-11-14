using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ShopOnline.Models.DBObjects;
using System.ComponentModel.DataAnnotations;

namespace ShopOnline.Models
{
    public class TvaModel
    {
        public Guid IdTva { get; set; }
        [Display(Name = "Cota TVA")]
      
        public decimal? Tval { get; set; }
        [ValidateNever]
        public Guid IdCategory { get; set; }

        public virtual Category IdCategoryNavigation { get; set; } = null!;
    }
}
