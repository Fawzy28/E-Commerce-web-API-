using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public enum OrderStatus { newOrder = 1 , shipped = 2 }                   //enum for the status
    public class Order
    {
        [Key , Required]
        public int Id { get; set; }

        
        public string status { get; set; } = OrderStatus.newOrder.ToString();

        public Customer Customer { get; set; }
        public List<Order_Product> Order_Products { get; set; }
    }
}
