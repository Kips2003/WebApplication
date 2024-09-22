using WebApi.DTO.Product;

namespace WebApi.Services.Product;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProductsAsync();
    Task<ProductDto> GetProductByIdAsync(int id);
    Task<ProductDto> CreateProductAsync(Models.Product product);
    public Task<IEnumerable<ProductDto>> GetProductByTitleAsync(string title);
    Task<ProductDto> UpdateProductAsync(int id, ProductDto products);
    Task<bool> DeleteProductAsync(int id);
}