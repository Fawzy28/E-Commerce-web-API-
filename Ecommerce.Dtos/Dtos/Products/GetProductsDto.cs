using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Dtos.Dtos.Products
{
    public class GetProductsDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Discription { get; set; }

        public int Price { get; set; }

        public string categoryName { get; set; }  
    }
}
