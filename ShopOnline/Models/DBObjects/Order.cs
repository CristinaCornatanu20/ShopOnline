using System;
using System.Collections.Generic;

namespace ShopOnline.Models.DBObjects
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public Guid IdOrder { get; set; }
        public string IdUser { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public DateTime? OrderDate { get; set; }

        public virtual AspNetUser IdUserNavigation { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
