using Ecommerce.Dtos.AuthDtos;
using Ecommerce.Services.Interfaces.AuthInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ecommerce.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {

        private IRoleServices RoleServices;
        public RolesController(IRoleServices _RoleServices)
        {
            RoleServices = _RoleServices;
        }


        /// get all roles 
        // GET: api/<RolesController>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllRoles()
        {
            var res = await RoleServices.GetAllRoles();
            if (res != null)
            {
                return Ok(res);
            }
            return NotFound();
        }



        /// check and create new role *******************
        // POST api/<RolesController>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateRole (CreateOrUpdateRoleDto Dto)
        {
            if (ModelState.IsValid)
            {
                var role = RoleServices.roleCheck_Creation(Dto);
                if (role != null) { return Ok(); }

            }
            return BadRequest(); 
        }



        ///update role
        // PUT api/<RolesController>/5
        [HttpPut("{oldName}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateRole (CreateOrUpdateRoleDto Dto , string oldName)
        {
            if (ModelState.IsValid)
            {
                var role = RoleServices.UpdateRole(Dto, oldName);
                if (role != null) { return Ok("updated"); }
            }
            return BadRequest();
        }


        ///delete role
        // DELETE api/<RolesController>/5
        [HttpDelete("{roleName}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteRole (string roleName)
        {
            var role = RoleServices.DeleteRole(roleName);
                if (role != null) { return Ok("deleted"); }
                return BadRequest(); 
        }
    }
}
