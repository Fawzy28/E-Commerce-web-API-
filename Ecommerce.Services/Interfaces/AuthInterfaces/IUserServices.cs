using Ecommerce.Dtos.AuthDtos;
using Ecommerce.Ecommerce.Dtos.AuthDtos;
using Ecommerce.Models.auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Interfaces.AuthInterfaces
{
    public interface IUserServices
    {
        Task<CustomizedUser> IsExist(string Email);
        Task<CustomizedUser> CreateUser(RegisterDto Dto);
        Task<bool> addRolesToUser(CustomizedUser user, RegisterDto dto);
        Task<CustomizedUser> isAuthenticated(LoggingDto Dto);
        Task<string> GenerateTooken(CustomizedUser user);
        Task<GetUsersDto> GetOneUser(ClaimsPrincipal Id);
        Task<CustomizedUser> UpdateUser(ClaimsPrincipal principal, UpdateUserDto dto);
        Task<CustomizedUser> DeleteUser(ClaimsPrincipal principal);
    }
}
