using WebApi.DTO.Order;

namespace WebApi.Services.Order;

public interface IOrderService
{
    public Task<OrderDto> GetOrderByIdAsync(int id);
    public Task<OrderDto> CreateOrderAsync(OrderCreateDto orderCreate);
    public Task<OrderDto> UpdateOrderAsync(int id, OrderCreateDto orderCreate);
    public Task<OrderDto> AddProgressAsync(int id);
    public Task<OrderDto> RemoveProgressAsync(int id);
    public Task<bool> DeleteOrderAsync(int id);
}