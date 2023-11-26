using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopOnline.Models.DBObjects
{
    public partial class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }
        public Product? Product { get; set; }
        public int Amount { get; set; }
        public string? ShoppingCartId { get; set; }
    }
}
