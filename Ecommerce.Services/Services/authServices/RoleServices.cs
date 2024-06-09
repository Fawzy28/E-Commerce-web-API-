using AutoMapper;
using Ecommerce.Dtos.AuthDtos;
using Ecommerce.Dtos.Dtos.Products;
using Ecommerce.Models.auth;
using Ecommerce.Services.Interfaces.AuthInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Ecommerce.Services.Services.authServices
{
    public class RoleServices : IRoleServices
    {
        private  RoleManager<IdentityRole> roleManager;
        private IMapper Mapper;
        public RoleServices(RoleManager<IdentityRole> _rolemanager , IMapper _Mapper)
        {
             roleManager = _rolemanager;
            Mapper = _Mapper;
        }


        //role check and create
        public  async Task<IdentityRole> roleCheck_Creation(CreateOrUpdateRoleDto dto)
        {
            string roleName = dto.Name;
            var role = await roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                return role;               
            }
            IdentityRole userRole = new IdentityRole(roleName);
            await roleManager.CreateAsync(userRole);
            return userRole;
        }


        //get all roles
        public async Task<List<GetRolesDto>> GetAllRoles()
        {
            var roles = roleManager.Roles.Select(r => Mapper.Map<GetRolesDto>(r)).ToList();
            if (! roles.IsNullOrEmpty())
            {
            return roles;
            }
            return null;
        }

        //update role
        public async Task<IdentityRole> UpdateRole(CreateOrUpdateRoleDto dto , string oldName)
        {
            var oldRole = await roleManager.FindByNameAsync(oldName);
            if ( oldRole != null) 
            {
                oldRole.Name = dto.Name;
                var res = await roleManager.UpdateAsync(oldRole);
                if (res.Succeeded)
                { return oldRole; }
            }
            return null;
        }

        //delete role
        public async Task<IdentityRole> DeleteRole (string roleName)
        {
            var delRole = await roleManager.FindByNameAsync(roleName);
            if (delRole != null)
            {
                var res = await roleManager.DeleteAsync(delRole);     
            if (res.Succeeded)
                { return delRole; }
            }
            return null;
        }

       
    }
}
//{
//"email": "user_ahmed2gmail.com",
//  "password": "ahmed123456789@A",
//  "username": "FAWZY",
//  "nickName": "af"
//}


//eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJhZG1pbl9haG1lZG1vaGFtZWRAdXV1LmNvbSIsIlJvbGUiOlsidXNlciIsImFkbWluIl0sImV4cCI6MTcxMzUyNzQ5OX0.5yBwSZVCAKuWFt4q0TTgME41iIsQLre_0qWgYdr2vFw