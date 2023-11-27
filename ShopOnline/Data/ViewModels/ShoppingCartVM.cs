using ShopOnline.Data.Cart;

namespace ShopOnline.Data.ViewModels
{
    public class ShoppingCartVM
    {
        public ShoppingCart? ShoppingCart { get; set; }
        public decimal? ShoppingCartTotal { get; set; }
    }
}
