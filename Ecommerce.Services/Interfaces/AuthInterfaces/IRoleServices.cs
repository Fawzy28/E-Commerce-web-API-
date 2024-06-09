using Ecommerce.Dtos.AuthDtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Interfaces.AuthInterfaces
{
    public interface IRoleServices
    {
        Task<IdentityRole> roleCheck_Creation(CreateOrUpdateRoleDto dto);
        Task<List<GetRolesDto>> GetAllRoles();
        Task<IdentityRole> UpdateRole(CreateOrUpdateRoleDto dto, string newName);
        Task<IdentityRole> DeleteRole(string roleName);

    }
}
