using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApi.Services.Product;

public interface IProductRepository
{
    IEnumerable<Models.Product> GetProductsAsync();
    Models.Product GetProductByIdAsync(int id);
    Task<Models.Product> CreateProductAsync(Models.Product product);
    Task<IEnumerable<Models.Product>> GetProductByTitleAsync(string title);
    Task<IEnumerable<WebApi.Models.Product>> GetProductByCategoryIdAsync(int categoryId);
    Task<IEnumerable<WebApi.Models.Product>> GetProductByPriceRangeAsync(int startPrice, int endPrice);
    Task<IEnumerable<WebApi.Models.Product>> GetProductByFilterSectionAsynct(IEnumerable<Models.Product> categoryProducts, IEnumerable<Models.Product> priceProducts);
    Task<Models.Product> UpdateProductAsync(Models.Product product);
    Task<bool> DeleteProductAsync(int id);
}