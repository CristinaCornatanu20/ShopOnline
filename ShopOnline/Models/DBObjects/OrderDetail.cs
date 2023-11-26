using java.awt.print;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopOnline.Models.DBObjects
{
    public partial class OrderDetail
    {
       
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public Guid Id { get; set; }
            public int Amount { get; set; }
            public decimal Price { get; set; }

            [ForeignKey("ProductId")]
            public Guid ProductId { get; set; }
            public Product Product { get; set; }
            public Guid OrderId { get; set; }
            [ForeignKey("OrderId")]
            public Order Order { get; set; }
        
    }
}
