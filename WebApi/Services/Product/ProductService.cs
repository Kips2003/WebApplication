using Microsoft.Identity.Client;
using WebApi.DTO;
using WebApi.DTO.Product;
using WebApi.Models;
using WebApi.Services.Authentication;

namespace WebApi.Services.Product;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;

    public ProductService(IProductRepository productRepository, IUserRepository userRepository)
    {
        _productRepository = productRepository;
        _userRepository = userRepository;
    }
    
    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        var products = _productRepository.GetProductsAsync();
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
            Stock = p.Stock,
            Weight = p.Weight,
            UserId = p.UserId
        }).ToList();
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var product = _productRepository.GetProductByIdAsync(id);

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
            Weight = product.Weight,
            UserId = product.UserId
        };
    }

    public async Task<ProductDto> CreateProductAsync(int userId, Models.Product product)
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
            Tags = product.Tags,
            Thumbnail = product.Thumbnail,
            CategoryId = product.CategoryId,
            Stock = product.Stock,
            Weight = product.Weight,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
            QrCode = product.QrCode,
            BarCode = product.BarCode,
            UserId = userId
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
            Weight = p.Weight,
            UserId = p.UserId
        }).ToList();
        
    }

    public async Task<AuthResponseDto> UpdateProductAsync(int userId, ProductDto products)
    {
        var product = _productRepository.GetProductByIdAsync(products.Id);
        if (product is null)
            return new AuthResponseDto { Success = false, Message = "This Product does not exist" };
            
        var user =await _userRepository.getUserById(userId);
        if (user is null)
            return new AuthResponseDto{Success = false, Message = "User does now exist"};

        if (products.UserId != userId && user.PrivilageId != 1 && user.PrivilageId != 2)
        {
            return new AuthResponseDto { Success = false, Message = "You do not have access to change this address" };
        }


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
        product.Weight = products.Weight;

        await _productRepository.UpdateProductAsync(product);

        return new AuthResponseDto { Success = true, Message = "Product updated successfullu" };
    }

    public async Task<IEnumerable<ProductDto>> GetProductByQueryAsync(ProductSearchDto searchDto)
    {
        var products = await _productRepository.GetProductsByQueryAsync(searchDto);
        
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
            Weight = p.Weight,
            UserId = p.UserId
        }).ToList();
    }

    public async Task<AuthResponseDto> DeleteProductAsync(int id)
    {
        if (id == null)
            return new AuthResponseDto { Success = false, Message = "Provided Number is null" };

        var product = _productRepository.GetProductByIdAsync(id);
        if (product is null)
            return new AuthResponseDto { Success = false, Message = "There is no Address on this ID" };

        await _productRepository.DeleteProductAsync(id);

        return new AuthResponseDto { Success = true, Message = "Product removed from the database" };
    }
}