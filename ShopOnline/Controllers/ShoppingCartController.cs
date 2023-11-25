using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ShopOnline.Models.DBObjects;
using ShopOnline.Repository;
using ShopOnline.Models;

namespace ShopOnline.Controllers
{
    public class ShoppingCartController : Controller
    {

        private readonly UserManager<AspNetUser> _userManager;
        private readonly OrderRepository _orderRepository;
        private readonly OrderDetailsRepository _orderDetailsRepository;

        public ShoppingCartController(
        UserManager<AspNetUser> userManager,
        OrderRepository orderRepository,
        OrderDetailsRepository orderDetailsRepository)
        {
            _userManager = userManager;
            _orderRepository = orderRepository;
            _orderDetailsRepository = orderDetailsRepository;

        }

        public IActionResult Index()
        {
            // Obține utilizatorul curent
            AspNetUser user = _userManager.GetUserAsync(User).Result;

            // Verifică dacă utilizatorul este autentificat și obține UserId
            if (user != null)
            {
                Guid userId = Guid.Parse(user.Id);
                var cartDetails = _shoppingCartService.GetShoppingCartDetails(userId);
                return View(cartDetails);
            }

            // Tratează cazul în care utilizatorul nu este autentificat
            return RedirectToAction("Login");
        }

        public IActionResult AddToCart(Guid productId, int quantity)
        {
            // Obține utilizatorul curent
            AspNetUser user = _userManager.GetUserAsync(User).Result;

            // Verifică dacă utilizatorul este autentificat și obține UserId
            if (user != null)
            {
                Guid userId = Guid.Parse(user.Id);
                _shoppingCartService.AddToCart(userId, productId, quantity);

                // Adaugă sau actualizează detaliile comenzii în funcție de necesități
                var order = _orderRepository.GetOrderByUserId(userId);
                if (order == null)
                {
                    // Dacă nu există comandă pentru utilizator, creează una nouă
                    order = new OrderModel
                    {
                        IdUser = userId,
                        // Alte proprietăți
                    };
                    _orderRepository.InsertOrder(order);
                }

                // Adaugă sau actualizează detaliile comenzii
                var orderDetails = _orderDetailsRepository.GetOrderDetailsByOrderId(order.IdOrder);
                if (orderDetails == null)
                {
                    // Dacă nu există detalii pentru comandă, creează unele noi
                    orderDetails = new OrderDetailsModel
                    {
                        IdOrder = order.IdOrder,
                        // Alte proprietăți
                    };
                    _orderDetailsRepository.InsertOrderDetails(orderDetails);
                }
                else
                {
                    // Dacă există detalii pentru comandă, adaugă sau actualizează produsul în detaliile comenzii
                    _orderDetailsRepository.InsertOrderDetails(order.IdOrder, productId, quantity);
                }

                return RedirectToAction("Index");
            }

            // Tratează cazul în care utilizatorul nu este autentificat
            return RedirectToAction("Login");
        }
    
}
}
