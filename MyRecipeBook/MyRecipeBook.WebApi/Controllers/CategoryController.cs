using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.Dtos.Category;
using MyRecipeBook.Application.Interfaces.AppServices;

namespace MyRecipeBook.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryAppService _categoryAppService;

    public CategoryController(ICategoryAppService categoryAppService)
    {
        _categoryAppService = categoryAppService;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllAsync()
    {
        var categories = await _categoryAppService.GetAllAsync();
        return Ok(categories);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var category = await _categoryAppService.GetByIdAsync(id);
        if (category == null)
            return NotFound();

        return Ok(category);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryDto input)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdCategory = await _categoryAppService.CreateAsync(input);
        return Ok(createdCategory);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateCategoryDto input)
    {
        var updatedCategory = await _categoryAppService.UpdateAsync(id, input);
        return Ok(updatedCategory);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _categoryAppService.DeleteAsync(id);
        return NoContent();
    }
}