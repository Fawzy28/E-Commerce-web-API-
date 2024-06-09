using Ecommerce.Dtos.Dtos.Products;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Data;


namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductServices productServices;
        public ProductsController(IProductServices _productServices)
        {
            productServices = _productServices;
        }



        // GET: api/<ProductsController>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllProducts()
        {
            
            var productsDtos = await productServices.GetAllProducts(); 
            if(! productsDtos.IsNullOrEmpty())                                       //check if the return is null or empty list
            {
                return Ok(productsDtos);
            }
            return NotFound("The resource cannot be found");
        }

        // GET api/<ProductsController>/id
        [HttpGet("id/{id}")]
        [Authorize]
        public async Task<IActionResult> GetOneProduct(int id)
        {
            var productDto = await productServices.GetOneProduct(id);
            if (productDto != null)
            { return Ok(productDto); }
            return NotFound("The resource cannot be found");
        }

        // GET api/<ProductsController>/categoryName
        [HttpGet("category/{categoryName}")]
        [Authorize]


        public async Task<IActionResult> GetProductsBycategory(string categoryName)
        {
            var productsDto = await productServices.GetProductByCateg(categoryName);
            if (! productsDto.IsNullOrEmpty())
            { return Ok(productsDto); }
            return NotFound("The resource cannot be found");
        }

        // POST api/<ProductsController>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> createProduct(CreateOrUpdateProductDto Dto )
        {
            if (ModelState.IsValid)
            {
                var responseDto = await productServices.CreateProduct(Dto);
                if (responseDto.status == PStatus.created.ToString())
                {
                    return Ok(responseDto);
                }
                else
                    return BadRequest(responseDto);
            }
            return BadRequest();
        }

        // PUT api/<ProductsController>/id
        [HttpPut]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> updateProduct (CreateOrUpdateProductDto Dto, int id)
        {
            if (ModelState.IsValid)
            {
                var responseDto = await productServices.updateProduct(Dto, id);
                return Ok(responseDto);
            }
            else
            {
                return BadRequest();
            }   
        }

        // DELETE api/<ProductsController>/id

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deletedObject =  await productServices.DeleteProduct(id);
            if(deletedObject!=null)
            {
                return Ok("Deleted");
            }
            else { return NotFound(); }
            
        }
    }
}
