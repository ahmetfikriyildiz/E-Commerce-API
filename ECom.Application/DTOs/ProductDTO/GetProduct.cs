using ECom.Application.DTOs.CategoryDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Application.DTOs.ProductDTO
{
    public class GetProduct : ProductBase
    {
        [Required]
        public Guid Id { get; set; }

        public GetCategory? Category { get; set; }

    }
}
