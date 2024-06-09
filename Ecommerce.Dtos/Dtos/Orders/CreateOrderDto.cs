using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Dtos.Dtos.Orders
{
    public class CreateOrderDto
    {
        public List<productQuantities> ProdQant { get; set; }             //in the db
    }
}
