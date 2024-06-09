using Ecommerce.Dtos.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Dtos.Dtos.categories
{
    public enum CStatus { created , notCreated , exist , updated , notUpdated }
    public class CreatedOrUpdatedCategoryDto
    {
        public int Id { get; set; }
        public string status { get; set; } = CStatus.created.ToString();               //not in the db
    }
}
