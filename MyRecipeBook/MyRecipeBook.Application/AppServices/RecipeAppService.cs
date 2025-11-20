using AutoMapper;
using MyRecipeBook.Application.Dtos.Recipe;
using MyRecipeBook.Application.Interfaces.AppServices;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories;

namespace MyRecipeBook.Application.AppServices;

public class RecipeAppService : IRecipeAppService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IMapper _mapper;

    public RecipeAppService(IRecipeRepository recipeRepository, IMapper mapper)
    {
        _recipeRepository = recipeRepository;
        _mapper = mapper;
    }

    public async Task<List<RecipeDto>> GetAllAsync()
    {
        var recipes = await _recipeRepository.GetAllAsync();
        return _mapper.Map<List<RecipeDto>>(recipes);
    }

    public async Task<RecipeDto> GetByIdAsync(Guid id)
    {
        var recipe = await _recipeRepository.GetByIdAsync(id) 
                     ?? throw new ArgumentNullException($"Recipe with id {id} not found");
        return _mapper.Map<RecipeDto>(recipe);
    }

    public async Task<RecipeDto> CreateAsync(CreateRecipeDto input)
    {
        var recipe = _mapper.Map<Recipe>(input);
        await _recipeRepository.AddAsync(recipe);
        await _recipeRepository.SaveChangesAsync();
        return _mapper.Map<RecipeDto>(recipe);
    }

    public async Task<RecipeDto> UpdateAsync(Guid id, UpdateRecipeDto input)
    {
        var recipe = await _recipeRepository.GetByIdAsync(id) 
                     ?? throw new ArgumentNullException($"Recipe with Id {id} not found.");

        recipe.Name = input.Name;
        recipe.Description = input.Description;
        recipe.CookingTime = input.CookingTime;
        recipe.PreparationTime = input.PreparationTime;
        recipe.NumberOfServings = input.NumberOfServings;
        
        await _recipeRepository.UpdateAsync(recipe);
        await _recipeRepository.SaveChangesAsync();
        return _mapper.Map<RecipeDto>(recipe);
    }

    public async Task DeleteAsync(Guid id)
    {
        var recipe = await _recipeRepository.GetByIdAsync(id) 
                     ?? throw new ArgumentNullException($"Recipe with Id {id} not found.");
        await _recipeRepository.RemoveAsync(recipe);
        await _recipeRepository.SaveChangesAsync();
    }
}