using WebApi.DTO.Category;

namespace WebApi.Services.Category;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
    {
        var categories = await _categoryRepository.GetCategoriesAsync();

        return categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name
        });
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(id);
        if (category is null)
            return null;

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto)
    {
        var category = new Models.Category
        {
            Name = categoryCreateDto.Name
        };

        category = await _categoryRepository.CreateCategoryAsync(category);

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task<CategoryDto> UpdateCategoryAsync(int id, CategoryCreateDto categoryCreateDto)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(id);
        if (category is null)
            return null;

        category = await _categoryRepository.UpdateCategoryAsync(category);

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        return await _categoryRepository.DeleteCategoryAsync(id);
    }
}