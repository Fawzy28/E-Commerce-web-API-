using AutoMapper;
using Ecommerce.Dtos.Dtos.categories;
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
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Services.CategoryServices
{
    public class CategoriesServices : ICategoryServices
    {
        IMapper Mapper;
        ICategoryRepository categoryRepository;
        public CategoriesServices(IMapper _Mapper, ICategoryRepository _categoryRepository)
        {
            categoryRepository = _categoryRepository;
            Mapper = _Mapper;
        }

        //Get all categories

        public async Task<List<GetAllCategoriesDto>> GetAllCategories()
        {
            var returnedTask = await categoryRepository.GetAll();
            if (returnedTask != null)
            {
                var query = returnedTask.Select(c => Mapper.Map<GetAllCategoriesDto>(c)).ToList();
                return (query);
            }
            return null;
        }


        //Get one category
        public async Task<GetAllCategoriesDto> GetOneCategory(int Id)
        {

            var returnedTask = await categoryRepository.GetOne(Id);
            if (!returnedTask.IsNullOrEmpty())
            {
                var query = returnedTask.Select(c => Mapper.Map<GetAllCategoriesDto>(c)).FirstOrDefault();
                return (query);
            }
            return null;
        }

        //create category

        public async Task<CreatedOrUpdatedCategoryDto> CreateCategory(CreateOrUpdateCategoryDto Dto)
        {

            Category category = Mapper.Map<Category>(Dto);
            var resp = Mapper.Map<CreatedOrUpdatedCategoryDto>(category);

            var res = await categoryRepository.GetAll();
            var isExist = await res.Where(c => c.Name == Dto.Name).FirstOrDefaultAsync();
            if ( isExist != null)
            {
                resp = Mapper.Map<CreatedOrUpdatedCategoryDto>(isExist);
                resp.status = CStatus.exist.ToString();
                return resp;

            }
            var addedCategory = await categoryRepository.add(category);
            if (addedCategory != null)
            {
                resp = Mapper.Map<CreatedOrUpdatedCategoryDto>(addedCategory);
                resp.status = CStatus.created.ToString();
                return resp;
            }
            else
            {
                resp.status = CStatus.notCreated.ToString();
                return resp;
            }
        }




        //update category 




        //remove category

        public async Task<Category> DeleteCategory(int Id)
        {
            var returnedTask = await categoryRepository.GetOne(Id);
            if (returnedTask != null) 
            { 
                var Delcategory = returnedTask.FirstOrDefault();
                await categoryRepository.delete(Delcategory); 
                return Delcategory;

            }
            else
            {
                return null;
            }
        }

    }
}
