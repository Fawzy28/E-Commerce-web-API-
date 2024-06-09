using Ecommerce.Services.Interfaces;
using Ecommerce.Services.Services.CategoryServices;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using Ecommerce.Services.Services.CategoryServices;
using Ecommerce.Dtos.Dtos.categories;
using Ecommerce.Services.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoriesController : ControllerBase
    {
        private ICategoryServices categoriesServices;
        public CategoriesController(ICategoryServices _categoriesServices)
        {
            categoriesServices = _categoriesServices;
        }



        // GET: api/<CategoriesController>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllCategories()
        {

            var CategoriesDtos = await categoriesServices.GetAllCategories();
            if (! CategoriesDtos.IsNullOrEmpty())                                       //check if the return is null or empty list
            {
                return Ok(CategoriesDtos);
            }
            return NotFound("The resource cannot be found");
        }


        // GET api/<CategoriesController>/id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetOneCategory(int id)
        {
            var CategoriesDto = await categoriesServices.GetOneCategory(id);
            if (CategoriesDto != null)
            { return Ok(CategoriesDto); }
            return NotFound("The resource cannot be found");
        }


        // POST api/<CategoriesController>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> createCategory(CreateOrUpdateCategoryDto Dto)
        {
            if (ModelState.IsValid)
            {
                var responseDto = await categoriesServices.CreateCategory(Dto);
                if (responseDto.status == CStatus.created.ToString())
                {
                    return Ok(responseDto);
                }
                else
                    return BadRequest(responseDto);
            }
            return BadRequest();
        }



        // DELETE api/<CategoriesController>/id
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCategory (int id)
        {
            var deletedObject = await categoriesServices.DeleteCategory(id);
            if (deletedObject != null)
            {
                return Ok("Deleted");
            }
            else { return NotFound(); }
        }
    }
}
