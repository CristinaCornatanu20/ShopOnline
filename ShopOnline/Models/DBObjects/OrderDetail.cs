﻿using System;
using System.Collections.Generic;

namespace ShopOnline.Models.DBObjects
{
    public partial class OrderDetail
    {
        public Guid IdOrderDetails { get; set; }
        public Guid IdOrder { get; set; }
        public Guid IdProduct { get; set; }
        public Guid IdTva { get; set; }
        public decimal? PriceTva { get; set; }
        public int? Quantity { get; set; }
        public decimal? PriceTotal { get; set; }

        public virtual Order IdOrderNavigation { get; set; } = null!;
        public virtual Product IdProductNavigation { get; set; } = null!;
        public virtual Tva IdTvaNavigation { get; set; } = null!;
    }
}
