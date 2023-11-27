using System.ComponentModel.DataAnnotations;

namespace ShopOnline.Models
{
    public class ApplicationUser
    {
        [Display(Name = "Full name")]
        public string? FullName { get; set; }
    }
}
