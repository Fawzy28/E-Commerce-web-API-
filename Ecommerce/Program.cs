
using Ecommerce.Context;
using Ecommerce.Context.Context;
using Ecommerce.Models.auth;
using Ecommerce.Repositories.Interfaces;
using Ecommerce.Repositories.Repositories;
using Ecommerce.Services.Interfaces;
using Ecommerce.Services.Interfaces.AuthInterfaces;
using Ecommerce.Services.Mapper;
using Ecommerce.Services.Services.authServices;
using Ecommerce.Services.Services.CategoryServices;
using Ecommerce.Services.Services.OrdersServices;
using Ecommerce.Services.Services.ProductServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using User.Ecommerce.Services.Services.authServices;

namespace Ecommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddDbContext<EcContext>(op => { op.UseSqlServer(builder.Configuration.GetConnectionString("UsersCS")); }) ;
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IOrder_productRepository, Order_ProductRepository>();

            builder.Services.AddScoped<IOrderServices, OrderServices>();
            builder.Services.AddScoped<IProductServices, ProductService>();
            builder.Services.AddScoped<ICategoryServices, CategoriesServices>();
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<IRoleServices, RoleServices>();

            builder.Services.AddAutoMapper(s => s.AddProfile(new MapperProfile()));


            builder.Services.AddIdentity<CustomizedUser, IdentityRole>()
                            .AddEntityFrameworkStores<EcContext>();

           
            builder.Services.AddAuthentication(op => {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(op =>
           {
               op.TokenValidationParameters = new TokenValidationParameters()
               {
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:jwtToken"])),
                   ValidateIssuerSigningKey = true,
                   ValidateAudience = false,
                   ValidateIssuer = false

               };
           });

            builder.Services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {

                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
            });


            //adding policies 
            builder.Services.AddCors(op => {
                op.AddPolicy("AllowForAll", policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
                op.AddPolicy("AllowForThis", policy => policy.WithOrigins("http://127.0.0.1:5500").AllowAnyHeader().AllowAnyMethod());
            });

            builder.Services.AddEndpointsApiExplorer();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JWTAuthDemo v1"));
            }

            app.UseCors("AllowForAll");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}