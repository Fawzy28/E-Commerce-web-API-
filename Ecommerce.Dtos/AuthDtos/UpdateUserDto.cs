using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Dtos.AuthDtos
{
    public class UpdateUserDto
    {
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? Username { get; set; }
        public string? BirthDate { get; set; }
        public string? Address { get; set; }
        public int? Card { get; set; }
    }
}
