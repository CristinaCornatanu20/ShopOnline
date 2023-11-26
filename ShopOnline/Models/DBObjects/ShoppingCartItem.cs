using System;
using System.Collections.Generic;

namespace ShopOnline.Models.DBObjects
{
    public partial class ShoppingCartItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string UserId { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual AspNetUser User { get; set; } = null!;
    }
}
