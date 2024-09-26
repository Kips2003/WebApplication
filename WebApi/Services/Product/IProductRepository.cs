namespace WebApi.Services.Product;

public interface IProductRepository
{
    IEnumerable<Models.Product> GetProductsAsync();
    Models.Product GetProductByIdAsync(int id);
    Task<Models.Product> CreateProductAsync(Models.Product product);
    Task<IEnumerable<Models.Product>> GetProductByTitleAsync(string title);
    Task<Models.Product> UpdateProductAsync(Models.Product product);
    Task<bool> DeleteProductAsync(int id);
}