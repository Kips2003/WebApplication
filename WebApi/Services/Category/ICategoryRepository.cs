namespace WebApi.Services.Category;

public interface ICategoryRepository
{
    public Task<IEnumerable<Models.Category>> GetCategoriesAsync();
    public Task<Models.Category> GetCategoryByIdAsync(int id);
    public Task<Models.Category> CreateCategoryAsync(Models.Category category);
    public Task<Models.Category> UpdateCategoryAsync(Models.Category category);
    public Task<bool> DeleteCategoryAsync(int id);
}