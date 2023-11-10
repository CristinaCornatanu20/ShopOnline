using System;
using System.Collections.Generic;

namespace ShopOnline.Models.DBObjects
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
            Tvas = new HashSet<Tva>();
        }

        public Guid IdCategory { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Tva> Tvas { get; set; }
    }
}
