using System;
using System.Collections.Generic;

namespace ShopOnline.Models.DBObjects
{
    public partial class Tva
    {
        public Tva()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public Guid IdTva { get; set; }
        public decimal? Tva1 { get; set; }
        public Guid IdCategory { get; set; }

        public virtual Category IdCategoryNavigation { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
