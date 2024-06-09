using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Dtos.Dtos.Orders
{
    public class GetAllOrdersDto
    {
        public int Id { get; set; }
        public string status { get; set; }

        public List<productQuantities> productQuantities { get; set; }

        public string CustomerId { get; set; }
        
    }
}
