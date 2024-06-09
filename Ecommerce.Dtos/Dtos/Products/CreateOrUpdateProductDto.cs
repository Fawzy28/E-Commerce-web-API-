using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Dtos.Dtos.Products
{
    public class CreateOrUpdateProductDto
    {
        public string Name { get; set; }

        public string Discription { get; set; }

        public int Price { get; set; }

        public int  categoryId { get; set; }
    }
}
