using ShopOnline.Models.DBObjects;

namespace ShopOnline.Models
{
    public class ShoppingCartItemModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        // Alte informații relevante despre articolul din coș pot fi adăugate aici

        public virtual Product Product { get; set; }
    }
}
