using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Dtos.AuthDtos
{
    public class CreateOrUpdateRoleDto
    {
        [MaxLength(50) , MinLength(2)]
        public string Name { get; set; }
    }
}
