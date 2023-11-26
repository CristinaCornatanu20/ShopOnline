using Microsoft.AspNetCore.Identity;
using ShopOnline.Data;
using ShopOnline.Models;
using ShopOnline.Models.DBObjects;

namespace ShopOnline.Repository
{
    public class ShoppingCartItemRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShoppingCartItemRepository(ApplicationDbContext dbContext, UserManager<AspNetUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<ShoppingCartItemModel> GetAllShoppingCartItems()
        {
            var userId = GetCurrentUserId();
            var itemsFromDb = _dbContext.ShoppingCartItems
                .Where(item => item.UserId == userId)
                .ToList();

            return itemsFromDb.Select(MapDbObjectToModel).ToList();
        }

        public void AddToShoppingCart(Guid productId, int quantity)
        {
            var userId = GetCurrentUserId();
            var existingItem = _dbContext.ShoppingCartItems
                .FirstOrDefault(item => item.UserId == userId && item.ProductId == productId);

            var product = _dbContext.Products.FirstOrDefault(p => p.IdProduct == productId);
            var tva = _dbContext.Tvas.FirstOrDefault(t => t.IdCategory == product.IdCategory);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var newCartItem = new ShoppingCartItem
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity,
                    TotalPrice = (decimal)((product.Price + (product.Price * tva.Tva1 / 100)) * quantity),
                };

                _dbContext.ShoppingCartItems.Add(newCartItem);
            }

            _dbContext.SaveChanges();
        }

        public void UpdateShoppingCartItem(Guid cartItemId, int quantity)
        {
            var cartItem = _dbContext.ShoppingCartItems.FirstOrDefault(item => item.Id == cartItemId);

            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                _dbContext.SaveChanges();
            }
        }

        public void RemoveFromShoppingCart(Guid cartItemId)
        {
            var cartItem = _dbContext.ShoppingCartItems.FirstOrDefault(item => item.Id == cartItemId);

            if (cartItem != null)
            {
                _dbContext.ShoppingCartItems.Remove(cartItem);
                _dbContext.SaveChanges();
            }
        }

        private string GetCurrentUserId()
        {
            var claimsPrincipal = _httpContextAccessor.HttpContext.User;

            // Obțineți UserId utilizând UserManager
            return _userManager.GetUserId(claimsPrincipal);
        }

        private ShoppingCartItemModel MapDbObjectToModel(ShoppingCartItem cartItem)
        {
            return new ShoppingCartItemModel
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                UserId = cartItem.UserId,
                Quantity = cartItem.Quantity,
                TotalPrice = cartItem.TotalPrice,
            };
        }
    }
}
  