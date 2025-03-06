namespace ECom.Application.DTOs.ProductDTO
{
    public class ProductBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
    }
}
