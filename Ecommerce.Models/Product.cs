using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Product
    {
        [Key,Required]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Discription { get; set; }
       
        public int Price { get; set; }
        public Category CategoryName { get; set; }
        public List<Order_Product>? Order_Product { get; set; }

    }
}