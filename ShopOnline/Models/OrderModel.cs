using ShopOnline.Models.DBObjects;

namespace ShopOnline.Models
{
    public class OrderModel
    {
        public Guid IdOrder { get; set; }
        public string IdUser { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public DateTime? OrderDate { get; set; }

       public virtual AspNetUser IdUserNavigation { get; set; }
    }
}
