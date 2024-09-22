using Microsoft.EntityFrameworkCore;
using WebApi.Data;

namespace WebApi.Services.Product;

public class ProductRepository : IProductRepository
{
    private readonly DataContext _context;

    public ProductRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Models.Product>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Models.Product> GetProductByIdAsync(int id)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Models.Product> CreateProductAsync(Models.Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<IEnumerable<Models.Product>> GetProductByTitleAsync(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return new List<Models.Product>();
        }

        return await _context.Products
            .Where(p => p.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
            .ToListAsync();    
    }

    public async Task<Models.Product> UpdateProductAsync(Models.Product product)
    {
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}