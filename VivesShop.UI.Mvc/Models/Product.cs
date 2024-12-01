using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VivesShop.UI.Mvc.Models
{
    [Table(nameof(Product))]
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required double Price { get; set; }
    }
}
