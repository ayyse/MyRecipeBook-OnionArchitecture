using AutoMapper;
using MyRecipeBook.Application.Dtos.Recipe;
using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Application.Mappings;

public class RecipeProfile : Profile
{
    public RecipeProfile()
    {
        CreateMap<Recipe, RecipeDto>();
        CreateMap<CreateRecipeDto, Recipe>();
        CreateMap<UpdateRecipeDto, Recipe>();
    }
}