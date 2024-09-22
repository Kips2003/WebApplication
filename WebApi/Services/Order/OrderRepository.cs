using Microsoft.EntityFrameworkCore;
using WebApi.Data;

namespace WebApi.Services.Order;

public class OrderRepository : IOrderRepository
{
    private readonly DataContext _context;

    public OrderRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<Models.Order> GetOrderBuIdAsync(int id)
    {
        return await _context.Orders.Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Models.Order> CreateOrderAsync(Models.Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        _context.Entry(order).Reload();
        return order;
    }

    public async Task<Models.Order> UpdateOrderAsync(Models.Order order)
    {
        _context.Entry(order).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<bool> DeleteOrderAsync(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order is null)
            return false;

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        return true;
    }
}