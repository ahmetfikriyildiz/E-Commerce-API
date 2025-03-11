using ECom.Application.DTOs.ProductDTO;

namespace ECom.Application.DTOs.CategoryDTO
{
    public class GetCategory : CategoryBase
    {
        public Guid Id { get; set; }
        public ICollection<GetProduct> Products { get; set; }
    }
}
