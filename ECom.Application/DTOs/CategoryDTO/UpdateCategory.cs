using System.ComponentModel.DataAnnotations;

namespace ECom.Application.DTOs.CategoryDTO
{
    public class UpdateCategory : CategoryBase
    {
        [Required]
        public Guid Id { get; set; }
    }
}
