using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.Order;
using WebApi.Services.Order;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _order;

    public OrderController(IOrderService order)
    {
        _order = order;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _order.GetOrderByIdAsync(id);
        if (order is null)
            return NotFound();

        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(OrderCreateDto orderCreate)
    {
        var order = await _order.CreateOrderAsync(orderCreate);
        return CreatedAtAction(nameof(GetOrderById), new { Id = order.Id }, order);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, OrderCreateDto orderCreate)
    {
        var order = await _order.UpdateOrderAsync(id, orderCreate);
        if (order is null)
            return NotFound();

        return Ok(order);
    }

    [HttpPut("add/{id}")]
    public async Task<IActionResult> AddProgress(int id)
    {
        var order = await _order.GetOrderByIdAsync(id);
        if (order is null)
            return NotFound();

        order = await _order.AddProgressAsync(id);
        return Ok(order);
    }
    
    [HttpPut("remove/{id}")]
    public async Task<IActionResult> RemoveProgress(int id)
    {
        var order = await _order.GetOrderByIdAsync(id);
        if (order is null)
            return NotFound();

        order = await _order.RemoveProgressAsync(id);
        return Ok(order);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _order.DeleteOrderAsync(id);
        if (!order)
            return NotFound();

        return NoContent();
    }
}