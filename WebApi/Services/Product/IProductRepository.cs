namespace WebApi.Services.Product;

public interface IProductRepository
{
    Task<IEnumerable<Models.Product>> GetProductsAsync();
    Task<Models.Product> GetProductByIdAsync(int id);
    Task<Models.Product> CreateProductAsync(Models.Product product);
    public Task<IEnumerable<Models.Product>> GetProductByTitleAsync(string title);
    Task<Models.Product> UpdateProductAsync(Models.Product product);
    Task<bool> DeleteProductAsync(int id);
}