using System.ComponentModel.DataAnnotations;

namespace ECom.Application.DTOs.ProductDTO
{
    public class UpdateProduct : ProductBase
    {
        [Required]
        public Guid Id { get; set; }
    }
}
