using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models.DBObjects;
using ShopOnline.Repository;

namespace ShopOnline.Controllers
{
    public class ShoppingCartItemController : Controller
    {
        private readonly ShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly UserManager<AspNetUser> _userManager;

        public ShoppingCartItemController(ShoppingCartItemRepository shoppingCartItemRepository, UserManager<AspNetUser> userManager)
        {
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            // Obțineți toate elementele din coș pentru utilizatorul curent (aici trebuie să furnizați UserId)
            var userId = _userManager.GetUserId(User);
            //var shoppingCartItems = _shoppingCartItemRepository.GetAllShoppingCartItems(userId);

            return View();
        }

        public IActionResult AddToCart(Guid productId, int quantity)
        {
            // Adăugați produsul în coșul de cumpărături
            // (aici trebuie să furnizați UserId
            var userId = _userManager.GetUserId(User);
            //_shoppingCartItemRepository.AddToShoppingCart(userId, productId, quantity);

            return RedirectToAction("Index");
        }

        public IActionResult UpdateCartItem(Guid cartItemId, int quantity)
        {
            // Actualizați cantitatea pentru un anumit element din coș
            _shoppingCartItemRepository.UpdateShoppingCartItem(cartItemId, quantity);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(Guid cartItemId)
        {
            // Eliminați un element din coșul de cumpărături
            _shoppingCartItemRepository.RemoveFromShoppingCart(cartItemId);

            return RedirectToAction("Index");
        }
    }
}


