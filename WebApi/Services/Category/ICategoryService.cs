using WebApi.DTO.Category;
using WebApi.DTO.Product;

namespace WebApi.Services.Category;

public interface ICategoryService
{
    public Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    public Task<CategoryDto> GetCategoryByIdAsync(int id);
    public Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto);
    public Task<CategoryDto> UpdateCategoryAsync(int id, CategoryCreateDto categoryCreateDto);
    public Task<bool> DeleteCategoryAsync(int id);
}