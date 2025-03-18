using ECom.Application.DTOs.ProductDTO;
using ECom.Application.Services.Interfaces;
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await productService.AddAsync(product);
            return result.Succes? Ok(result) : BadRequest(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateProduct product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await productService.UpdateAsync(product);
            return result.Succes ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await productService.DeleteAsync(id);
            return result.Succes ? Ok(result) : BadRequest(result);
        }
    }
}
