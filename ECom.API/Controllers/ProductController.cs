using ECom.Application.DTOs.ProductDTO;
using ECom.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var data = await productService.GetAllAsync();
            return data.Any() ? Ok(data) : NotFound(data);
        }
        [HttpGet("Single/{id}")]
        public async Task<IActionResult> GetSingle(Guid id)
        {
            var data = await productService.GetByIdAsync(id);
            return data != null ? Ok(data) : NotFound(data);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateProduct product)
        {

        }
    }
}
