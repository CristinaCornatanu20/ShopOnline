using Microsoft.EntityFrameworkCore;
using ShopOnline.Data;
using ShopOnline.Models.DBObjects;

namespace ShopOnline.Repository
{
    public class OrdersService : IOrdersService
    {
        private readonly ApplicationDbContext _context;
        public OrdersService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId, string userRole)
        {
            var orders = await _context.Orders.Include(n => n.OrderDetails).ThenInclude(n => n.Product).Where(n => n.UserId == userId).ToListAsync();
            if (userRole != "Admin")
            {
                orders = orders.Where(n => n.UserId == userId).ToList();
            }
            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderDetail()
                {
                    Amount = item.Amount,
                    ProductId = item.Product.IdProduct,
                    OrderId = order.Id,
                    Price = item.Product.Price
                };


                await _context.OrderDetails.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }
    }
}

