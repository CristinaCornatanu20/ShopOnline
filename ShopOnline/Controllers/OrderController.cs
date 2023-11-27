using Microsoft.AspNetCore.Mvc;
using ShopOnline.Data.Cart;
using ShopOnline.Data.ViewModels;
using ShopOnline.Repository;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using ShopOnline.Models.DBObjects;
using ShopOnline.Data;

namespace ShopOnline.Controllers
{
    public class OrderController : Controller
    {
        private readonly ProductRepository _prodService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;
        private readonly ApplicationDbContext _context;

        public OrderController(ProductRepository prodService, ShoppingCart shoppingCart, 
            IOrdersService ordersService, ApplicationDbContext dbContext)
        {
            _prodService = prodService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
            _context = dbContext;
        }

        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(response);
        }
        public Task<IActionResult> AddItemShoppingCart(Guid id)
        {
            var item = _prodService.GetProductById1(id);
            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(ShoppingCart)));
        }
        public Task<IActionResult> RemoveItemShoppingCart(Guid id)
        {
            var item =  _prodService.GetProductById(id);
            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return Task.FromResult<IActionResult>(RedirectToAction(nameof(ShoppingCart)));
        }


        public async Task<IActionResult> CompleteOrder()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmail = User.Identity.Name;

            var newOrder = new Order
            {
                Id = Guid.NewGuid(),
                Email = userEmail, // Setează adresa de email a utilizatorului sau lasă câmpul gol, în funcție de necesități
                UserId = userId,
                
            };

            // Obține elementele din coș
            var cartItems = _shoppingCart.GetShoppingCartItems();

            // Adaugă detalii în comandă
            foreach (var cartItem in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Id = Guid.NewGuid(),
                    Amount = cartItem.Amount,
                    Price = cartItem.Product.Price*cartItem.Amount, // sau poți utiliza alt preț specific, depinde de logica ta
                    ProductId = cartItem.Product.IdProduct, // sau cartItem.ProductId, depinde de structura ShoppingCartItem
                   
                    OrderId = newOrder.Id // Setează ID-ul comenzii pentru detaliul comenzii
                };

                _context.OrderDetails.Add(orderDetail);
            }

            // Adaugă comanda în baza de date
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            // Eliberează coșul de cumpărături
            await _shoppingCart.ClearShoppingCartAsync();

            // Redirectează către o pagină de confirmare a comenzii
            return View("OrderCompleted");
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await _ordersService.GetOrdersByUserIdAsync(userId, userRole);

            return View(orders);
        }
        
    }
}
