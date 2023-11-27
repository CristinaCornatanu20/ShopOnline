using Microsoft.AspNetCore.Mvc;
using ShopOnline.Data.Cart;

namespace ShopOnline.Data
{
    public class ShoppingCartSummary: ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            return View("ShoppingCartSummary", items.Count);
        }
    }
}
