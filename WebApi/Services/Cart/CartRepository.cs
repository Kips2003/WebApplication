using Microsoft.EntityFrameworkCore;
using WebApi.Data;

namespace WebApi.Services.Cart;

public class CartRepository : ICartRepository
{
    private readonly DataContext _context;

    public CartRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<Models.Cart> GetCartByIdAsync(int id)
    {
        return await _context.Carts.Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Models.Cart> CreateCartAsync(Models.Cart cart)
    {
        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();

        return cart;
    }

    public async Task<Models.Cart> UpdateCartAsync(Models.Cart cart)
    {
        _context.Entry(cart).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return cart;
    }

    public async Task<bool> DeleteCartAsync(int id)
    {
        var cart = await _context.Carts.FindAsync(id);
        if (cart == null)
            return false;

        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync();

        return true;
    }
}