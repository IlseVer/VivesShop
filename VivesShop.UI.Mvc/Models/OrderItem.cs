using System.ComponentModel.DataAnnotations.Schema;

namespace VivesShop.UI.Mvc.Models
{  
    [Table(nameof(OrderItem))]
    public class OrderItem
    {
        public int Id { get; set; }
        public int ShopItemId { get; set; }
        public Product? ShopItem { get; set; } 
        public int Quantity { get; set; }

    }
}
