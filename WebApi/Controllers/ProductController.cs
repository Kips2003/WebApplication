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
    public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto request)
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

            var createdProduct = await _product.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductCreateDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            // Retrieve the existing product
            var existingProduct = await _product.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            // Update the product fields
            existingProduct.Title = request.Title;
            existingProduct.Description = request.Description;
            existingProduct.Price = request.Price;
            existingProduct.Stock = request.Stock;
            existingProduct.CategoryId = request.CategoryId;
            existingProduct.Tags = request.Title.Split(" ");
            existingProduct.Weight = request.Weight;
            existingProduct.Width = request.Width;
            existingProduct.Depth = request.Depth;
            existingProduct.Height = request.Height;
            existingProduct.Images = request.Images;
            existingProduct.Thumbnail = request.Thumbnail;
            existingProduct.BarCode = request.BarCode;
            existingProduct.QrCode = request.QrCode;
            
            /*existingProduct.Meta.BarCode = request.Meta.BarCode;
            existingProduct.Meta.QrCode = request.Meta.QrCode;
            existingProduct.Meta.UpdatedAt = DateTime.UtcNow;*/ // Update timestamp

            await _product.UpdateProductAsync(id, existingProduct);

            return NoContent(); // 204 No Content for a successful update
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var success = await _product.DeleteProductAsync(id);
        if (!success) return NotFound();

        return NoContent();
    }
}