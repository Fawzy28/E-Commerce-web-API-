using AutoMapper;
using Ecommerce.Dtos.AuthDtos;
using Ecommerce.Dtos.Dtos.categories;
using Ecommerce.Dtos.Dtos.Orders;
using Ecommerce.Dtos.Dtos.Products;
using Ecommerce.Ecommerce.Dtos.AuthDtos;
using Ecommerce.Models;
using Ecommerce.Models.auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {                                                     //for products
            CreateMap<Product,GetProductsDto>()
               .ReverseMap();

            CreateMap<CreateOrUpdateProductDto, Product>()                
                .ReverseMap();
            CreateMap<CreateOrUpdateProductDto,Category>()
                .ForMember(dest => dest.Id , opt => opt.MapFrom(src => src.categoryId))
                .ReverseMap();

            CreateMap<Product, CreatedOrUpdatedProductDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

                                                               //for categories

            CreateMap<Category, GetAllCategoriesDto>()
                .ReverseMap();
            CreateMap<CreateOrUpdateCategoryDto, Category>()
                .ReverseMap();
            CreateMap<Category, CreatedOrUpdatedCategoryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

                                                        //for orders

            CreateMap<Order, GetAllOrdersDto>()
                 .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.status))
                  //.ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))  => can't map child propeties
                  .ReverseMap();

            CreateMap<Customer, GetAllOrdersDto>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Id))
                  .ReverseMap();




            //if it doesn't work :

            CreateMap<productQuantities, Order_Product>()
                .ReverseMap();

            CreateMap<UpdateOrderDto, Order>()
                 .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.OrderStatus))
                 .ReverseMap();

            CreateMap<Order,CreatedOrUpdatedOrderDto>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

                                                          // for users and roles

            CreateMap<CustomizedUser, RegisterDto>()
                 .ReverseMap();

            CreateMap<CustomizedUser,GetUsersDto>()
                .ReverseMap();

            CreateMap<UpdateUserDto,CustomizedUser>()
                .ReverseMap();

            CreateMap<CreateOrUpdateRoleDto, IdentityRole>()
                .ReverseMap();

            CreateMap<IdentityRole , GetRolesDto>()
                .ReverseMap();

        }
    }
}
