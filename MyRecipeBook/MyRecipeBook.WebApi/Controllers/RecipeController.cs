using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.Dtos.Recipe;
using MyRecipeBook.Application.Interfaces.AppServices;

namespace MyRecipeBook.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipeController : ControllerBase
{
    private readonly IRecipeAppService _recipeAppService;

    public RecipeController(IRecipeAppService recipeAppService)
    {
        _recipeAppService = recipeAppService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllAsync()
    {
        var recipes = await _recipeAppService.GetAllAsync();
        return Ok(recipes);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var recipe = await _recipeAppService.GetByIdAsync(id);
        if (recipe == null)
            return NotFound();

        return Ok(recipe);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateRecipeDto input)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdRecipe = await _recipeAppService.CreateAsync(input);
        return Ok(createdRecipe);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateRecipeDto input)
    {
        var updatedRecipe = await _recipeAppService.UpdateAsync(id, input);
        return Ok(updatedRecipe);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _recipeAppService.DeleteAsync(id);
        return NoContent();
    }
}