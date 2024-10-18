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
    Task<IEnumerable<ProductDto>> GetProductByQueryAsync(ProductSearchDto searchDto);
    Task<AuthResponseDto> DeleteProductAsync(int id);
}