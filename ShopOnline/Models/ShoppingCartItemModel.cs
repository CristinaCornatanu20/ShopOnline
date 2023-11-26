using java.awt.print;
using ShopOnline.Models.DBObjects;
using System.ComponentModel.DataAnnotations;

namespace ShopOnline.Models
{
    public class ShoppingCartItemModel
    {
        [Key]
        public int Id { get; set; }
        public Product? Product { get; set; }
        public int Amount { get; set; }
        public string? ShoppingCartId { get; set; }
    }
}
