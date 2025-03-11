using AutoMapper;
using ECom.Application.DTOs;
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
    public class ProductService(IGeneric<Product> productInterface,IMapper mapper) : IProductService
    {
        public async Task<ServiceResponse> AddAsync(CreateProduct product)
        {
            var mappedData = mapper.Map<Product>(product);
            int result = await productInterface.AddAsync(mappedData);
            return result > 0 ? new ServiceResponse(true, "Product created") :
                new ServiceResponse(false, "Product failed to be created");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await productInterface.DeleteAsync(id);
            return result > 0 ? new ServiceResponse(true, "Product Deleted") :
                new ServiceResponse(false, "Product failed to be deleted");

        }

        public async Task<IEnumerable<GetProduct>> GetAllAsync()
        {
            var rawData = await productInterface.GetAllAsync();
            if (!rawData.Any()) return [];

            return mapper.Map<IEnumerable<GetProduct>>(rawData);
        }

        public async Task<GetProduct> GetByIdAsync(Guid id)
        {
            var rawData = await productInterface.GetByIdAsync(id);
            if (rawData==null) return new GetProduct();

            return mapper.Map<GetProduct>(rawData);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateProduct product)
        {
            var mappedData = mapper.Map<Product>(product);
            int result = await productInterface.UpdateAsync(mappedData);
            return result > 0 ? new ServiceResponse(true, "Product updated") :
                new ServiceResponse(false, "Product failed to be updated");
        }
    }
}
