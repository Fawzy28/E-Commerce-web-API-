using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Dtos.Dtos.Products
{
    public enum PStatus { created = 1 , notCreated = 2 , exist = 3 , updated = 4 , notUpdated = 5 }
    public class CreatedOrUpdatedProductDto
    {
        public int Id { get; set; }
        public string status { get; set; }             //not in the db
    }
}
