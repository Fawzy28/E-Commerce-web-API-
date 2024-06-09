using AutoMapper;
using Ecommerce.Dtos.AuthDtos;
using Ecommerce.Ecommerce.Dtos.AuthDtos;
using Ecommerce.Models.auth;
using Ecommerce.Services.Interfaces.AuthInterfaces;
using Ecommerce.Services.Mapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using User.Ecommerce.Services.Services.authServices;

namespace Ecommerce.Services.Services.authServices
{
    public class UserServices : IUserServices
    {
        private UserManager<CustomizedUser> userManager;
        private IMapper Mapper;
        private RoleManager<IdentityRole> roleManger;
        IConfiguration configuration;
        public UserServices(UserManager<CustomizedUser> _userManager, IMapper _Mapper, RoleManager<IdentityRole> _roleManager, IConfiguration _configuration)
        {
            userManager = _userManager;
            Mapper = _Mapper;
            roleManger = _roleManager;
            configuration = _configuration;
        }

        //check for user existence//////////////
        public async Task<CustomizedUser> IsExist(string Email)
        {
            var user = await userManager.FindByEmailAsync(Email);
            if (user != null)
            {
                return (user);
            }
            return null;
        }

        //create user////////////////

        public async Task<CustomizedUser> CreateUser(RegisterDto Dto)
        {
            var newUser = Mapper.Map<CustomizedUser>(Dto);
            var res = await userManager.CreateAsync(newUser, Dto.Pass);          //new user is tracked now
            if (res.Succeeded)
            {
                return (newUser);
            }
            return (null);
        }


        //add roles to user///////////////
        public async Task<bool> addRolesToUser(CustomizedUser user, RegisterDto dto)
        {

            var roles = roleManger.Roles.ToList();                      //get all roles
            if (!roles.IsNullOrEmpty())
            {
                foreach (var r in roles)
                {

                    if (dto.Email.Contains(r.Name))
                    {
                        var res = await userManager.AddToRoleAsync(user, r.Name);
                        if (!res.Succeeded) { return false; }
                        return true;
                    }

                    var ress = await userManager.AddToRoleAsync(user, "user");
                    if (!ress.Succeeded) { return false; }

                    return true;
                }
            }
            return false;
        }




        //check if user authenticated//////////
        public async Task<CustomizedUser> isAuthenticated(LoggingDto Dto)
        {
            var user = await userManager.FindByEmailAsync(Dto.Email);
            if (user == null)
            {
                return (null);
            }
            var res = await userManager.CheckPasswordAsync(user, Dto.Password);
            if (res == false)
            { return null; }

            return user;

        }



        //generate tooken for user/////////////
        public async Task<string> GenerateTooken(CustomizedUser user)
        {

            var myClaims = new List<Claim>()                           //generate user's claims for tooken
            {
                new Claim(ClaimTypes.NameIdentifier,(user.Id).ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim("Address",user.Address),
                new Claim(ClaimTypes.DateOfBirth,user.BirthDate),
                new Claim("Card",(user.Card).ToString()),

            };

            var Roles = await userManager.GetRolesAsync(user);
            if (Roles.IsNullOrEmpty()) { return null; }
            foreach (var item in Roles)                                     //addroles to claims
            {
                var roleClaim = new Claim(ClaimTypes.Role, item);
                myClaims.Add(roleClaim);

            }


            //tooken settings 

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:jwtToken"]));

            var tokenSettings = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(2),
                claims: myClaims,
                signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
                                                     );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenSettings);
            return token;

        }

        //get one user///////////
        public async Task<GetUsersDto> GetOneUser(ClaimsPrincipal principal)
        {
            var res = await userManager.GetUserAsync(principal);
            if (res != null)
            {
                var userDto = Mapper.Map<GetUsersDto>(res);
                return userDto;
            }
            else return null;
        }

        ///update user////////////

        public async Task<CustomizedUser> UpdateUser(ClaimsPrincipal principal, UpdateUserDto dto)
        {
            var user = await userManager.GetUserAsync(principal);
            if (user != null)
            {
                var updateduser = Mapper.Map<CustomizedUser>(dto);
                if (dto.Address != null) { user.Address = updateduser.Address; }
                if (dto.Username != null) { user.UserName = updateduser.UserName; }
                if (dto.Email != null) { user.Email = updateduser.Email;}

                if (dto.PasswordHash != null)
                {
                    PasswordHasher<CustomizedUser> passwordHasher = new PasswordHasher<CustomizedUser>();     //hashing the password manually
                    user.PasswordHash = passwordHasher.HashPassword(user, updateduser.PasswordHash);
                }
                if (dto.Card != null) { user.Card = updateduser.Card; }

                var res = await userManager.UpdateAsync(user);
                if (res != null)
                {
                    return user;
                }

            }
            return null;

        }


        /// delete user ////////

        public async Task<CustomizedUser> DeleteUser(ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal);
            if (user != null)
            {
                var res = await userManager.DeleteAsync(user);
                if (res != null) { return (user); }
            }
            return null;
        }
    }
}

