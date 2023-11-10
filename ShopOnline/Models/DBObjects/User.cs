using System;
using System.Collections.Generic;

namespace ShopOnline.Models.DBObjects
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public Guid IdUser { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public int? Role { get; set; }
        public byte[]? Image { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
