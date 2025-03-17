using AutoMapper;
using ECom.Application.DTOs;
using ECom.Application.DTOs.CategoryDTO;
using ECom.Application.DTOs.ProductDTO;
using ECom.Application.Services.Interfaces;
using ECom.Domain.Entities;
using ECom.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Application.Services.Implementations
{
    public class CategoryService(IGeneric<Category> categoryInterface,IMapper mapper) : ICategoryService
    {
        public async Task<ServiceResponse> AddAsync(CreateCategory category)
        {
            var mappedData = mapper.Map<Category>(category);
            int result = await categoryInterface.AddAsync(mappedData);
            return result > 0 ? new ServiceResponse(true, "Category created") :
                new ServiceResponse(false, "Category failed to be created");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await categoryInterface.DeleteAsync(id);
            //if (result == 0)
            //    return new ServiceResponse(false, "Category failed to be deleted");

            return result > 0 ? new ServiceResponse(true, "Category Deleted") :
                new ServiceResponse(false, "Category not found or failed to be deleted");
        }

        public async Task<IEnumerable<GetCategory>> GetAllAsync()
        {
            var rawData = await categoryInterface.GetAllAsync();
            if (!rawData.Any()) return [];

            return mapper.Map<IEnumerable<GetCategory>>(rawData);
        }

        public async Task<GetCategory> GetByIdAsync(Guid id)
        {
            var rawData = await categoryInterface.GetByIdAsync(id);
            if (rawData == null) return new GetCategory();

            return mapper.Map<GetCategory>(rawData);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateCategory category)
        {
            var mappedData = mapper.Map<Category>(category);
            int result = await categoryInterface.UpdateAsync(mappedData);
            return result > 0 ? new ServiceResponse(true, "Category updated") :
                new ServiceResponse(false, "Category failed to be updated");
        }
    }
}



   

