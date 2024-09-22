using WebApi.DTO.Product;
using WebApi.Models;

namespace WebApi.Services.Product;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        var products = await _productRepository.GetProductsAsync();
        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Title = p.Title,
            Width = p.Width,
            Height = p.Height,
            Depth = p.Depth,
            Description = p.Description,
            Price = p.Price,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt,
            BarCode = p.BarCode,
            QrCode = p.QrCode,
            Images = p.Images,
            Tags = p.Title.Split(" "),
            Thumbnail = p.Thumbnail,
            CategoryId = p.CategoryId,
            Stock = p.Stock,
            Reviews = p.Reviews,
            Weight = p.Weight
        }).ToList();
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);

        if (product is null)
            return null;

        return new ProductDto
        {
            Id = product.Id,
            Title = product.Title,
            Width = product.Width,
            Height = product.Height,
            Depth = product.Depth,
            Description = product.Description,
            Price = product.Price,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
            BarCode = product.BarCode,
            QrCode = product.QrCode,
            Images = product.Images,
            Tags = product.Title.Split(" "),
            Thumbnail = product.Thumbnail,
            CategoryId = product.CategoryId,
            Stock = product.Stock,
            Reviews = product.Reviews,
            Weight = product.Weight
        };
    }

    public async Task<ProductDto> CreateProductAsync(Models.Product product)
    {
        product = await _productRepository.CreateProductAsync(product);

        return new ProductDto
        {
            Id = product.Id,
            Title = product.Title,
            Width = product.Width,
            Height = product.Height,
            Depth = product.Depth,
            Description = product.Description,
            Price = product.Price,
            Images = product.Images,
            Tags = product.Title.Split(" "),
            Thumbnail = product.Thumbnail,
            CategoryId = product.CategoryId,
            Stock = product.Stock,
            Reviews = product.Reviews,
            Weight = product.Weight,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
            QrCode = product.QrCode,
            BarCode = product.BarCode
        };
    }

    public async Task<IEnumerable<ProductDto>> GetProductByTitleAsync(string title)
    {
        var products = await _productRepository.GetProductByTitleAsync(title);

        if (products.Count() < 1)
        {
            return null;
        }

        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Title = p.Title,
            Height = p.Height,
            Width = p.Width,
            Depth = p.Depth,
            Description = p.Description,
            Price = p.Price,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt,
            BarCode = p.BarCode,
            QrCode = p.QrCode,
            Images = p.Images,
            Tags = p.Title.Split(" "),
            Thumbnail = p.Thumbnail,
            CategoryId = p.CategoryId,
            Stock = p.Stock,
            Reviews = p.Reviews,
            Weight = p.Weight
        }).ToList();    }

    public async Task<ProductDto> UpdateProductAsync(int id, ProductDto products)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product is null)
            return null;

        product.Title = products.Title;
        product.Height = products.Height;
        product.Width = products.Width;
        product.Depth = products.Depth;
        product.Description = products.Description;
        product.Price = products.Price;
        product.CreatedAt = products.CreatedAt;
        product.UpdatedAt = DateTime.Now;
        product.BarCode = products.BarCode;
        product.QrCode = products.QrCode;
        product.Images = products.Images;
        product.Tags = products.Tags;
        product.Thumbnail = products.Thumbnail;
        product.CategoryId = products.CategoryId;
        product.Stock = products.Stock;
        product.Reviews = products.Reviews;
        product.Weight = products.Weight;

        product = await _productRepository.UpdateProductAsync(product);

        return new ProductDto
        {
            Id = product.Id,
            Title = product.Title,
            Height = product.Height,
            Width = product.Width,
            Depth = product.Depth,
            Description = product.Description,
            Price = product.Price,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
            QrCode = product.QrCode,
            BarCode = product.BarCode,
            Images = product.Images,
            Tags = product.Title.Split(" "),
            Thumbnail = product.Thumbnail,
            CategoryId = product.CategoryId,
            Stock = product.Stock,
            Reviews = product.Reviews,
            Weight = product.Weight
        };
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        return await _productRepository.DeleteProductAsync(id);
    }
}