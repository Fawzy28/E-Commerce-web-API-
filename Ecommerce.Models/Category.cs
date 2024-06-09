using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Category
    {
        [Key,Required]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}
