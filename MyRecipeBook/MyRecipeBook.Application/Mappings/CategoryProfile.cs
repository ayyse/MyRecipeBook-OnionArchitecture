using AutoMapper;
using MyRecipeBook.Application.Dtos.Category;
using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Application.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();
    }
}