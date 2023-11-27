using ShopOnline.Models.DBObjects;

namespace ShopOnline.Data.ViewModels
{
    public class OrderViewModel
    {
        public List<Order> Order { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
