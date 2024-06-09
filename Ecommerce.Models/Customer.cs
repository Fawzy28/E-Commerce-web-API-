using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Customer                                     //we will attach the customer to the user by id 
    {
        [Key , Required]
        public string Id { get; set; }
        [MaxLength (50)]
        public string UserName { get; set; }
        [MaxLength (150)]
        public string Address { get; set; }
        public int Card { get; set; }
        public List<Order> Orders { get; set; }

    }
}
