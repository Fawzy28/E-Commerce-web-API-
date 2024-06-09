using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.auth
{
    public class CustomizedUser:IdentityUser
    {
        [MaxLength(150)]
        public string Address { get; set; }
        public int Card { get; set; }
        public string BirthDate { get; set; }
    }
}
