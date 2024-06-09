using Ecommerce.Dtos.Dtos.Orders;
using Ecommerce.Dtos.Dtos.Orders;
using Ecommerce.Services.Interfaces;
using Ecommerce.Services.Services.OrdersServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.ExceptionServices;
using System.Security.Claims;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrderServices orderServices;
        public OrdersController(IOrderServices _orderServices)
        {
            orderServices = _orderServices;
        }


        // GET: api/<OrdersController>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllOrders()
        {

            var ordersDtos = await orderServices.GetAllOrders();
            if (!ordersDtos.IsNullOrEmpty())                                       //check if the return is null or empty list
            {
                return Ok(ordersDtos);
            }
            return NotFound("The resource cannot be found");
        }

        // GET api/<OrdersController>/id
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetOneOrder(int id)
        {
            var OrderDto = await orderServices.GetOneOrder(id);
            if (OrderDto != null)
            { return Ok(OrderDto); }
            return NotFound("The resource cannot be found");
        }

        // POST api/<OrdersController>
        [HttpPost]
        [Authorize(Roles="admin" )]
        //[Authorize(Roles = "user")]
        public async Task<IActionResult> createOrder(CreateOrderDto Dto )
        {
            //  send the userid first to check if the acustomer with this id exists  => relate the order to the custome r
            //  if not exist create new one and relate it to the order
            if (ModelState.IsValid)
            {
                if (HttpContext.User is ClaimsPrincipal principal)
                {
                    if (principal.HasClaim(c => c.Type == "NameIdentifier")) ;
                    {
                        var responseDto = await orderServices.CreateOrder(Dto , principal);
                        if (responseDto.status == OStatus.created.ToString())
                        {
                            return Ok(responseDto);
                        }
                        else
                        return BadRequest(responseDto);
                    }
                }
            }
            return BadRequest();
        }

        // PUT api/<OrdersController>/id
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> updateOrder(int id, [FromBody] UpdateOrderDto Dto)
        {
            if (ModelState.IsValid)
            {
                var responseDto = await orderServices.updateOrder(id, Dto);
                return Ok(responseDto);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<OrdersController>/id
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var deletedObject = await orderServices.DeleteOrder(id);
            if (deletedObject != null)
            {
                return Ok("Deleted");
            }
            else { return NotFound(); }
        }
    }
}
