using AutoMapper;
using Ecommerce.Dtos.Dtos.Orders;
using Ecommerce.Dtos.Dtos.Products;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Ecommerce.Repositories.Repositories;
using Ecommerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Services.OrdersServices
{
    public class OrderServices:IOrderServices
    {
        IMapper Mapper;
        IOrderRepository OrderRepository;
        IOrder_productRepository OrderProductRepository;
        ICustomerRepository CustomerRepository;
        public OrderServices(IMapper _Mapper, IOrderRepository _orderRepository, IOrder_productRepository _OrderProductRepository , ICustomerRepository _CustomerRepository)
        {
            Mapper = _Mapper;
            OrderRepository = _orderRepository;
            OrderProductRepository = _OrderProductRepository;
            CustomerRepository = _CustomerRepository;
        }


        //Get all
        public async Task<List<GetAllOrdersDto>> GetAllOrders()
        {
            var returnedTask = await OrderRepository.GetAll();
            if (!returnedTask.IsNullOrEmpty())
            {
                var orders = returnedTask.Include(o => o.Order_Products)
                                         .Include(o => o.Customer);
                                         
                List<GetAllOrdersDto> Dtoslist = new List<GetAllOrdersDto>();
                foreach (var order in orders)
                {
                    var dto = Mapper.Map<GetAllOrdersDto>(order);                                      //status , customer id , orderid
                    dto.CustomerId = order.Customer.Id;
                    dto.productQuantities = Mapper.Map<List<productQuantities>>(order.Order_Products);     //map objects in list from objects in other list 
                    Dtoslist.Add(dto);
                }
                return Dtoslist;
            }
            return null;

        }



        //GetOne
        public async Task<GetAllOrdersDto> GetOneOrder(int Id)
        {
            var returnedTask = await OrderRepository.GetOne(Id);
            if (!returnedTask.IsNullOrEmpty())
            {
                var order = await returnedTask.Include(o => o.Order_Products).Include(o => o.Customer).FirstOrDefaultAsync();
                var dto = Mapper.Map<GetAllOrdersDto>(order);
                dto.CustomerId = order.Customer.Id;
                dto.productQuantities = Mapper.Map<List<productQuantities>>(order.Order_Products);
                return dto;
            }
            return null;
        }



        //create order

        public async Task<CreatedOrUpdatedOrderDto> CreateOrder(CreateOrderDto Dto , ClaimsPrincipal principal)
        {
            
            Order order = new Order();

            var customerId = principal.FindFirstValue(ClaimTypes.NameIdentifier);                  // to get te customerid from the httpcontext    
            var customer = await CustomerRepository.GetOne(customerId);
            if(customer.IsNullOrEmpty())
            {
                var newCustomer = new Customer()                                         //if the customer not exist create new one and get it's data from the user
                {
                    Id = customerId,
                    UserName = principal.FindFirstValue(ClaimTypes.Name),
                    Address = principal.FindFirstValue("Address"),                       //when order added the customer will be added and related to it 
                    Card = int.Parse(principal.FindFirstValue("Card"))
                };
                order.Customer = newCustomer;
            }
            else { order.Customer = await customer.FirstOrDefaultAsync(); }                //or if the customer exist relate it 

            order.Order_Products = Mapper.Map<List<Order_Product>>(Dto.ProdQant);              // to get the list of [{prodid , quantity},..]
            
            var addedOrder = await OrderRepository.add(order);
            if (addedOrder != null)
            {
                var respp = Mapper.Map<CreatedOrUpdatedOrderDto>(addedOrder);
                respp.status = OStatus.created.ToString();
                return respp;
            }
                     
            var resp = new CreatedOrUpdatedOrderDto() { status = OStatus.notCreated.ToString() };
            return resp;
            
        }


        //update order
        public async Task<CreatedOrUpdatedOrderDto> updateOrder(int Id,UpdateOrderDto Dto)
        {

            var returnedTask = await OrderRepository.GetOne(Id);
            if (!returnedTask.IsNullOrEmpty())
            {
                var oldOrder = await returnedTask.FirstOrDefaultAsync();

                oldOrder.status = Dto.OrderStatus;
                await OrderRepository.update(oldOrder);
                var respp = Mapper.Map<CreatedOrUpdatedOrderDto>(oldOrder);
                respp.status = OStatus.updated.ToString();
                return respp;
            }
            else
            {
                CreatedOrUpdatedOrderDto resp = new CreatedOrUpdatedOrderDto();
                resp.status = OStatus.notExist.ToString();
                return resp;
            }
        }


        //delete order

        public async Task<Order> DeleteOrder(int Id)
        {
            var returnedTask = await OrderRepository.GetOne(Id);
            if (!returnedTask.IsNullOrEmpty())
            {
                var DelOrder = returnedTask.FirstOrDefault();
                await OrderRepository.delete(DelOrder);
                return (DelOrder);
            }
            else
                return null;
        }




    }







}


