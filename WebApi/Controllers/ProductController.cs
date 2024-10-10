using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.Product;
using WebApi.Models;
using WebApi.Services.Product;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _product;
    private readonly IWebHostEnvironment _env;

    public ProductController(IProductService product, IWebHostEnvironment env)
    {
        _product = product;
        _env = env;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _product.GetProductsAsync();

        return Ok(products);
    }

    [HttpGet("WithId/{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = _product.GetProductByIdAsync(id);
        if (product is null)
            return NotFound();

        return Ok(product.Result);
    }

    [HttpGet("WithTitle/{title}")]
    public async Task<IActionResult> GetProductByTitle(string title)
    {
        var product = await _product.GetProductByTitleAsync(title);
        if (product is null)
            return NotFound();

        return Ok(product);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto request, int userId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var product = new Product
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                CategoryId = request.CategoryId,
                Tags = request.Title.Split(" "),
                Weight = request.Weight,
                Width = request.Width,
                Depth = request.Depth,
                Height = request.Height,
                Images = request.Images,
                Thumbnail = request.Thumbnail,
                QrCode = request.QrCode,
                BarCode = request.BarCode,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var createdProduct = await _product.CreateProductAsync(userId, product);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateProduct(int userId, ProductDto request)
    {
        var product = await _product.UpdateProductAsync(userId, request);

        if (!product.Success)
            return BadRequest(product.Message);

        return Ok(product.Message);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var success = await _product.DeleteProductAsync(id);
        if (!success.Success) return BadRequest(success.Message);

        return Ok(success.Message);
    }
}