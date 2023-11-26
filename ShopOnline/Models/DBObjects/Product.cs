using System;
using System.Collections.Generic;

namespace ShopOnline.Models.DBObjects
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            ShoppingCartItems = new HashSet<ShoppingCartItem>();
        }

        public Guid IdProduct { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid IdCategory { get; set; }
        public decimal Price { get; set; }
        public int? StockQuantity { get; set; }
        public string? Image { get; set; }

        public virtual Category IdCategoryNavigation { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
