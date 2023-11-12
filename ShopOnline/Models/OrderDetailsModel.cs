namespace ShopOnline.Models
{
    public class OrderDetailsModel
    {
        public Guid IdOrderDetails { get; set; }
        public Guid IdOrder { get; set; }
        public Guid IdProduct { get; set; }
        public Guid IdTva { get; set; }
        public decimal? PriceTva { get; set; }
        public int? Quantity { get; set; }
        public decimal? PriceTotal { get; set; }
    }
}
