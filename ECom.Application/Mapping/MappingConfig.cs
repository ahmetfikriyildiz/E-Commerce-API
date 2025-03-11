using AutoMapper;
using ECom.Application.DTOs.CategoryDTO;
using ECom.Application.DTOs.ProductDTO;
using ECom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Application.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<CreateCategory, Category>();
            CreateMap<CreateProduct, Product>();

            CreateMap<Category, GetCategory>();
            CreateMap<Product, GetProduct>();
        }
    }
}
