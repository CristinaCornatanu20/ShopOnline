using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopOnline.Models.DBObjects
{
    public partial class Order
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public AspNetUser User { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

    }
}
