using System.ComponentModel.DataAnnotations;

namespace ECom.Application.DTOs.CategoryDTO
{
    public class CategoryBase
    {
        [Required]
        public string? Name { get; set; }
    }
}
