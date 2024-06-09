using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Dtos.Dtos.Orders
{
    
    public class UpdateOrderDto
    {
        public string OrderStatus { get; set; } = "shipped";      //in db
        
    }
}
