using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApi.DTO.Order;
using WebApi.Models;

namespace WebApi.Services.Order;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _order;

    public OrderService(IOrderRepository order)
    {
        _order = order;
    }
    
    public async Task<OrderDto> GetOrderByIdAsync(int id)
    {
        var order = await _order.GetOrderBuIdAsync(id);
        if (order is null)
            return null;

        return new OrderDto
        {
            Id = order.Id,
            UserId = order.UserId,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            OrderItems = order.OrderItems.Select(io => new OrderItemDto
            {
                Id = io.Id,
                OrderId = io.OrderId,
                ProductId = io.ProductId,
                Quantity = io.Quantity,
                UnitPrice = io.UnitPrice
            }).ToList()
        };
    }

    public async Task<OrderDto> CreateOrderAsync(OrderCreateDto orderCreate)
    {
        var order = new Models.Order
        {
            UserId = orderCreate.UserId,
            OrderDate = DateTime.Now,
            TotalAmount = orderCreate.OrderItems.Sum(io => io.UnitPrice * io.Quantity),
            OrderItems = orderCreate.OrderItems.Select(oi => new OrderItem
            {
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice
            }).ToList()
        };

        order = await _order.CreateOrderAsync(order);

        return new OrderDto
        {
            Id = order.Id,
            UserId = order.UserId,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            OrderItems = order.OrderItems.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                OrderId = oi.OrderId,
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice
            }).ToList()
        };
    }

    public async Task<OrderDto> UpdateOrderAsync(int id, OrderCreateDto orderCreate)
    {
        var order = await _order.GetOrderBuIdAsync(id);
        if (order is null)
            return null;

        order.UserId = orderCreate.UserId;
        order.OrderItems = orderCreate.OrderItems.Select(io => new OrderItem
        {
            ProductId = io.ProductId,
            Quantity = io.Quantity,
            UnitPrice = io.UnitPrice
        }).ToList();

        order.TotalAmount = order.OrderItems.Sum(oi => oi.UnitPrice * oi.Quantity);
        order.OrderDate = DateTime.Now;

        order = await _order.UpdateOrderAsync(order);

        return new OrderDto
        {
            Id = order.Id,
            UserId = order.UserId,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            OrderItems = order.OrderItems.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                OrderId = oi.OrderId,
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice
            }).ToList()
        };
    }

    public async Task<OrderDto> AddProgressAsync(int id)
    {
        var order = await _order.GetOrderBuIdAsync(id);

        if (order is null)
            return null;

        order.OrderProgressId++;
        
        order = await _order.UpdateOrderAsync(order);

        return new OrderDto
        {
            Id = order.Id,
            UserId = order.UserId,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            OrderItems = order.OrderItems.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                OrderId = oi.OrderId,
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice
            }).ToList(),
            OrderProgressId = order.OrderProgressId
        };
    }

    public async Task<OrderDto> RemoveProgressAsync(int id)
    {
        var order = await _order.GetOrderBuIdAsync(id);

        order.OrderProgressId--;
        
        order = await _order.UpdateOrderAsync(order);

        return new OrderDto
        {
            Id = order.Id,
            UserId = order.UserId,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            OrderItems = order.OrderItems.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                OrderId = oi.OrderId,
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice
            }).ToList(),
            OrderProgressId = order.OrderProgressId
        };    
    }

    public async Task<bool> DeleteOrderAsync(int id)
    {
        return await _order.DeleteOrderAsync(id);
    }
}