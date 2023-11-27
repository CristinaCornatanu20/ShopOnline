using ShopOnline.Models.DBObjects;

namespace ShopOnline.Repository
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);
        Task<List<Order>> GetOrdersByUserIdAsync(string userId, string userRole);
    }
}
