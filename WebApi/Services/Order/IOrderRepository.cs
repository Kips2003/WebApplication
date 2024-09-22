namespace WebApi.Services.Order;

public interface IOrderRepository
{
    public Task<Models.Order> GetOrderBuIdAsync(int id);
    public Task<Models.Order> CreateOrderAsync(Models.Order order);
    public Task<Models.Order> UpdateOrderAsync(Models.Order order);
    public Task<bool> DeleteOrderAsync(int id);
}