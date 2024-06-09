using AutoMapper;
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

namespace Ecommerce.Services.Services.ProductServices
{
    public class ProductService : IProductServices
    {
        IMapper Mapper;
        IProductRepository productRepository;
        ICategoryRepository categoryRepository;
        public ProductService(IMapper _Mapper , IProductRepository _productRepository , ICategoryRepository _categoryRepository)
        {
            productRepository = _productRepository;
            Mapper = _Mapper;
            categoryRepository = _categoryRepository;
        }

        //Get all products

        public async Task<List<GetProductsDto>> GetAllProducts()
        {
            var returnedTask = await productRepository.GetAll();
            if (!returnedTask.IsNullOrEmpty())
            {
                var query = returnedTask.Include(p => p.CategoryName);
                List<GetProductsDto> GetDtos = new List<GetProductsDto>();
                foreach (var product in query)                                               //Deferred execution to get the related properties  
                {
                    var Dto = Mapper.Map<GetProductsDto>(product);
                    Dto.categoryName = product.CategoryName.Name;
                    GetDtos.Add(Dto);
                }
                    
                return (GetDtos);
            }
            return null;

        }

        //Get one product

        public async Task<GetProductsDto> GetOneProduct (int Id)
        {
            var returnedTask = await productRepository.GetOne(Id);
            if (! returnedTask.IsNullOrEmpty())
            {
                var query = await returnedTask.Include(p => p.CategoryName).FirstOrDefaultAsync();
                GetProductsDto dto = Mapper.Map<GetProductsDto>(query);
                dto.categoryName = query.CategoryName.Name;
                return (dto);
            }
            return null;

        }

         
        //Get products by category
        public async Task<List<GetProductsDto>> GetProductByCateg (string  CategName)
        {
            var returnedTask = await productRepository.GetByCateg(CategName);
            if ( ! returnedTask.IsNullOrEmpty())
            {
                var query = returnedTask.Include(p => p.CategoryName);
                List<GetProductsDto> GetDtos = new List<GetProductsDto>();
                foreach (var product in query)                                               //Deferred execution to get the related properties  
                {
                    var Dto = Mapper.Map<GetProductsDto>(product);
                    Dto.categoryName = product.CategoryName.Name;
                    GetDtos.Add(Dto);
                }

                return (GetDtos);
            }
            return null;
        }




        //create product
        public async Task<CreatedOrUpdatedProductDto> CreateProduct (CreateOrUpdateProductDto Dto)
        {

            Product product = Mapper.Map<Product>(Dto);
            var prodCategory = await categoryRepository.GetOne(Dto.categoryId);
            if (!prodCategory.IsNullOrEmpty())
            {
                product.CategoryName = await prodCategory.FirstOrDefaultAsync();     //to relate it with the category


                var res = await productRepository.GetOne(product.Id);
                var isExist = await res.FirstOrDefaultAsync();
                if (isExist != null)
                {
                    var resp = Mapper.Map<CreatedOrUpdatedProductDto>(isExist);
                    resp.status = PStatus.exist.ToString();
                    return resp;
                }
                var addedProduct = await productRepository.add(product);                        //it will return the object added
                if (addedProduct != null)
                {
                    var resp = Mapper.Map<CreatedOrUpdatedProductDto>(addedProduct);
                    resp.status = PStatus.created.ToString();
                    return resp;
                }

            }
            return new CreatedOrUpdatedProductDto() { status= PStatus.notCreated.ToString()};
            
        }





        //update product               ( update what u want in the product )
        public async Task<CreatedOrUpdatedProductDto> updateProduct(CreateOrUpdateProductDto Dto , int id)
        {

            var updatedProduct = Mapper.Map<Product>(Dto);
            var relatedCategory = await categoryRepository.GetOne(Dto.categoryId);
            if (!relatedCategory.IsNullOrEmpty())
            {
                updatedProduct.CategoryName = await relatedCategory.FirstOrDefaultAsync();
            
                var returnedTask = await productRepository.GetOne(id);           //hold the old product
                if (! returnedTask.IsNullOrEmpty())
                {
                    var oldProduct = await returnedTask.FirstOrDefaultAsync();
                
                    if (updatedProduct.Price != 0) { oldProduct.Price = updatedProduct.Price; };               //check first for each property if it is n't null (not given to update )
                    if (updatedProduct.Name != null) { oldProduct.Name = updatedProduct.Name; };
                    if (updatedProduct.Discription != null) { oldProduct.Discription = updatedProduct.Discription; };
                    if (updatedProduct.CategoryName.Id != 0) { oldProduct.CategoryName.Id = updatedProduct.CategoryName.Id; };
                    await productRepository.update(oldProduct);
                    var resp = Mapper.Map<CreatedOrUpdatedProductDto>(oldProduct);
                    resp.status = PStatus.updated.ToString();
                    return resp;
                }
                               //creation if the product doesn't exist
                await productRepository.update(updatedProduct);                               // note category must be exist
                var respp = Mapper.Map<CreatedOrUpdatedProductDto>(updatedProduct);           //get the id of last added project
                respp.status = PStatus.created.ToString();
                return respp;

            }

            return new CreatedOrUpdatedProductDto() { Id = id , status=PStatus.notUpdated.ToString()};
             


        
        }


        //remove product

        public async Task<Product> DeleteProduct (int Id)
        {          
            var returnedTask = await productRepository.GetOne(Id);
            if (!returnedTask.IsNullOrEmpty())
            {
                var DelProduct = returnedTask.FirstOrDefault();
                await productRepository.delete(DelProduct);
                return (DelProduct);
            }
            else
            {
                return null;
            }
        }
    }
}
