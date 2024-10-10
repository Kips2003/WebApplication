using WebApi.DTO;
using WebApi.DTO.Product;

namespace WebApi.Services.Product;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProductsAsync();
    Task<ProductDto> GetProductByIdAsync(int id);
    Task<ProductDto> CreateProductAsync(int userId, Models.Product product);
    public Task<IEnumerable<ProductDto>> GetProductByTitleAsync(string title);
    Task<AuthResponseDto> UpdateProductAsync(int id, ProductDto products);
    Task<AuthResponseDto> DeleteProductAsync(int id);
}