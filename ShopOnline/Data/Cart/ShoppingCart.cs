using java.awt.print;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;
using ShopOnline.Models.DBObjects;

namespace ShopOnline.Data.Cart
{
    public class ShoppingCart
    {
      
            public ApplicationDbContext _context { get; set; }

            public string ShoppingCartId { get; set; }

            public List<ShoppingCartItem> ShoppingCartItems { get; set; }
            public ShoppingCart(ApplicationDbContext context)
            {
                _context = context;
            }

            public static ShoppingCart GetShoppingCart(IServiceProvider services)
            {
                ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
                var context = services.GetService<ApplicationDbContext>();
                string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
                session.SetString("CartId", cartId);

                return new ShoppingCart(context) { ShoppingCartId = cartId };
            }

            public void AddItemToCart(Product product)
            {
                var shoppingCarItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Product.IdProduct == product.IdProduct && n.ShoppingCartId == ShoppingCartId);
                if (shoppingCarItem == null)
                {
                    shoppingCarItem = new ShoppingCartItem()
                    {
                        ShoppingCartId = ShoppingCartId,
                        Product = product,
                        Amount = 1
                    };
                    _context.ShoppingCartItems.Add(shoppingCarItem);
                }
                else
                {
                    shoppingCarItem.Amount++;
                }
                _context.SaveChanges();
            }

            public void RemoveItemFromCart(ProductModel product)
            {
                var shoppingCarItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Product.IdProduct == product.IdProduct && n.ShoppingCartId == ShoppingCartId);
                if (shoppingCarItem != null)
                {
                    if (shoppingCarItem.Amount > 1)
                    {
                        shoppingCarItem.Amount--;
                    }
                    else
                    {
                        _context.ShoppingCartItems.Remove(shoppingCarItem);
                    }
                }
                _context.SaveChanges();
            }
            public List<ShoppingCartItem> GetShoppingCartItems()
            {
                return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Product).ToList());
            }

            public decimal GetShoppingCartTotal()
            {
                var total = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Product.Price * n.Amount).Sum();
                return total;
            }

            public async Task ClearShoppingCartAsync()
            {
                var items = await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
                _context.ShoppingCartItems.RemoveRange(items);
                await _context.SaveChangesAsync();
            }

        }
    }
