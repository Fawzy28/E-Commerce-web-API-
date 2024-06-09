using Ecommerce.Dtos.AuthDtos;
using Ecommerce.Ecommerce.Dtos.AuthDtos;
using Ecommerce.Models.auth;
using Ecommerce.Services.Interfaces.AuthInterfaces;
using Ecommerce.Services.Services.authServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using System.Security.Claims;

namespace Ecommerce.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserServices userServices;
        private IRoleServices roleServices;
        public UserController(IUserServices _userServices , IRoleServices _roleServices)
        {
            userServices = _userServices;
            roleServices = _roleServices;
        }


        //registering          ****** for any one ******

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto Dto)
        {
            if (ModelState.IsValid)
            {
                var user = await userServices.IsExist(Dto.Email);                        //check if is exist first
                if (user != null)
                { return BadRequest("already exists"); }

                var addedUser = await userServices.CreateUser(Dto);                     //create
                if (addedUser == null)
                { return BadRequest("not created"); }


                //consider we have admin role and any other roles ,,
                //the user who has "admin" word in his email => he will get admin role   and the like for any other role
                //otherwise he will get "user" role 

                var res = await userServices.addRolesToUser(addedUser, Dto);
                if (res == false) { return BadRequest("created without roles"); }

                return Ok("registered!!");
            }
            return BadRequest();

        }

        //login               ****** for any one ******

        [HttpPost("Login")]
       
        public async Task<IActionResult> Login(LoggingDto Dto)
        {
            if (ModelState.IsValid)
            {
                var user = await userServices.isAuthenticated(Dto);
                if (user == null) { return Unauthorized("it seems that the email or password is wrong try again or register"); }

                var token = await userServices.GenerateTooken(user);
                if (token == null) { return BadRequest("token hasn't been generated"); }

                return Ok(token);
            }
            return BadRequest();
        }



        //GetoneUser
                         //  *****  for the user's account only  - by user or admin *****

        [HttpGet("GetOne")]
        [Authorize]
        public async Task<IActionResult> GetOneUser()
        { 
            if (HttpContext.User is ClaimsPrincipal principal)                             //claims prinicipal any thing have aclaims   ***** we do this to make sure that user have claims 
            {
                if (principal.HasClaim(c => c.Type == ClaimTypes.NameIdentifier ))                   //to make sure that user has claim of id 
                {
                    var res = await userServices.GetOneUser(principal);
                    if (res != null) { return Ok(res); }                                   //to make sure thet user has been found
                }
            }
            return BadRequest();
        }

        //UpdateUser  *****  for the user's account only  - by user / admin *****

        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UpdateUserDto dto)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.User is ClaimsPrincipal principal)
                {
                    if (principal.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
                    {
                        var res = await userServices.UpdateUser(principal, dto);
                        if (res != null) { return Ok("updated"); }
                    }
                }
            }
            return BadRequest();

        }


        //RemoveUser  ****  for the user's account only - by user / admin *****

        [HttpDelete("Delete")]
        [Authorize]

        public async Task<IActionResult> DeleteUser()
        {
            if (HttpContext.User is ClaimsPrincipal principal)
            {
                if (principal.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
                {
                    var res = await userServices.DeleteUser(principal);
                    if (res != null) { return Ok("Deleted"); }
                }
            }
            return BadRequest();

        }
    }



}
