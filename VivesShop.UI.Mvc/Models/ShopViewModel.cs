namespace VivesShop.UI.Mvc.Models
{
    public class ShopViewModel
    {
        public List<Product> ShopItems { get; set; } = new List<Product>();
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
