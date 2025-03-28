﻿using ECom.Application.DTOs.CategoryDTO;
using ECom.Application.DTOs.ProductDTO;
using ECom.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var data = await categoryService.GetAllAsync();
            return data.Any() ? Ok(data) : NotFound(data);
        }
        [HttpGet("Single/{id}")]
        public async Task<IActionResult> GetSingle(Guid id)
        {
            var data = await categoryService.GetByIdAsync(id);
            return data != null ? Ok(data) : NotFound(data);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateCategory category)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var result = await categoryService.AddAsync(category);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateCategory category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await categoryService.UpdateAsync(category);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await categoryService.DeleteAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
