using ECom.Application.DTOs;
using ECom.Application.DTOs.CategoryDTO;
using ECom.Application.DTOs.ProductDTO;

namespace ECom.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategory>> GetAllAsync();
        Task<GetCategory> GetByIdAsync(Guid id);
        Task<ServiceResponse> AddAsync(CreateCategory category);
        Task<ServiceResponse> UpdateAsync(UpdateCategory category);
        Task<ServiceResponse> DeleteAsync(Guid id);

    }



}
