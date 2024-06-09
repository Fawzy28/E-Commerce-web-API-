using Ecommerce.Dtos.Dtos.Orders;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Interfaces
{
    public interface IOrderServices
    {
        Task<List<GetAllOrdersDto>> GetAllOrders();
        Task<GetAllOrdersDto> GetOneOrder(int Id);
        Task<CreatedOrUpdatedOrderDto> CreateOrder(CreateOrderDto Dto, ClaimsPrincipal principal);
        Task<CreatedOrUpdatedOrderDto> updateOrder(int Id,UpdateOrderDto Dto);
        Task<Order> DeleteOrder(int Id);


    }
}
