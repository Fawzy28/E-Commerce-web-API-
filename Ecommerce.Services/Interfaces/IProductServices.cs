using Ecommerce.Dtos.Dtos.Products;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Interfaces
{
    public interface IProductServices
    {
        Task<List<GetProductsDto>> GetAllProducts();
        Task<GetProductsDto> GetOneProduct(int Id);
        Task<List<GetProductsDto>> GetProductByCateg(string CategName);
        Task<CreatedOrUpdatedProductDto> CreateProduct(CreateOrUpdateProductDto Dto);
        Task<CreatedOrUpdatedProductDto> updateProduct(CreateOrUpdateProductDto Dto, int id);
        Task<Product> DeleteProduct(int Id);

    }
}
