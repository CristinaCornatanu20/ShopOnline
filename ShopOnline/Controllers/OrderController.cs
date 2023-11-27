using Microsoft.AspNetCore.Mvc;
using ShopOnline.Data.Cart;
using ShopOnline.Data.ViewModels;
using ShopOnline.Repository;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace ShopOnline.Controllers
{
    public class OrderController : Controller
    {
        private readonly ProductRepository _prodService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;

        public OrderController(ProductRepository prodService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _prodService = prodService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
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
        public async Task<IActionResult> AddItemShoppingCart(Guid id)
        {
            var item = _prodService.GetProductById(id);
            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> RemoveItemShoppingCart(Guid id)
        {
            var item =  _prodService.GetProductById(id);
            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }


        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            string toAddress = userEmailAddress;
            string subject = "Order Confimation!";

            StringBuilder body = new StringBuilder();
            body.AppendLine("\nThank you for ordering on our shop!");

            foreach (var item in items)
            {
                body.AppendLine($"- {item.Product.Name}");
            }

            

            SendEmail(toAddress, subject, body.ToString());

            return View("OrderCompleted");
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await _ordersService.GetOrdersByUserIdAsync(userId, userRole);

            return View(orders);
        }
        public void SendEmail(string toAddress, string subject, string body)
        {

            // Set the SMTP server details
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("cornateanucristina@gmail.com", "jqmifsfdshkupzlu");

            // Create a message to send
            MailMessage message = new MailMessage();
            message.From = new MailAddress("cornateanucristina@gmail.com");
            message.To.Add(toAddress);
            message.Subject = subject;
            message.Body = body;

            // Send the message
            client.Send(message);
        }
    }
}
