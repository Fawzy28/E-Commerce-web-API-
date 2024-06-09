using Ecommerce.Dtos.Dtos.categories;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Interfaces
{
    public interface ICategoryServices
    {
        Task<List<GetAllCategoriesDto>> GetAllCategories();
        Task<GetAllCategoriesDto> GetOneCategory(int Id);
        Task<CreatedOrUpdatedCategoryDto> CreateCategory(CreateOrUpdateCategoryDto Dto);
        Task<Category> DeleteCategory(int Id);

    }
}
