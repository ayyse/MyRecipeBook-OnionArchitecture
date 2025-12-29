using AutoMapper;
using MyRecipeBook.Application.Dtos.Category;
using MyRecipeBook.Application.Interfaces.AppServices;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories;

namespace MyRecipeBook.Application.AppServices;

public class CategoryAppService : ICategoryAppService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryAppService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    public async Task<List<CategoryDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return _mapper.Map<List<CategoryDto>>(categories);
    }
    
    public async Task<List<CategoryDto>> GetParentCategoriesAsync()
    {
        var categories = await _categoryRepository.GetParentCategoriesAsync();
        return _mapper.Map<List<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id) 
                     ?? throw new ArgumentNullException($"Category with id {id} not found");
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto input)
    {
        var category = _mapper.Map<Category>(input);
        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveChangesAsync();
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> UpdateAsync(Guid id, UpdateCategoryDto input)
    {
        var category = await _categoryRepository.GetByIdAsync(id) 
                       ?? throw new ArgumentNullException($"Category with Id {id} not found.");

        category.Name = input.Name;
        category.Description = input.Description;
        category.ParentCategoryId = input.ParentCategoryId;
        
        await _categoryRepository.UpdateAsync(category);
        await _categoryRepository.SaveChangesAsync();
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task DeleteAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id) 
                       ?? throw new ArgumentNullException($"Category with Id {id} not found.");
        await _categoryRepository.RemoveAsync(category);
        await _categoryRepository.SaveChangesAsync();
    }
}