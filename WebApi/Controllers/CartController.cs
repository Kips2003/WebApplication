using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.Cart;
using WebApi.Models;
using WebApi.Services.Cart;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        var carts = await _cartService.GetCartAsync();

        return Ok(carts);
    }

    [HttpGet("withUserId/{userId}")]
    public async Task<IActionResult> GetCartByUserId(int userId)
    {
        var carts = await _cartService.GetCartByUserIdAsync(userId);

        return Ok(carts);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCartById(int id)
    {
        var cart = _cartService.GetCartByIdAsync(id);
        if (cart is null)
            return NotFound();

        return Ok(cart);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCart(CartCreateDto cartCreate)
    {
        var cart = await _cartService.CreateCartAsync(cartCreate);
        return CreatedAtAction(nameof(GetCartById), new { id = cart.Id }, cart);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCart(int id, CartCreateDto cartCreate)
    {
        var cart = await _cartService.UpdateCartAsync(id, cartCreate);
        if (cart is null)
            return NotFound();

        return Ok(cart);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCart(int id)
    {
        var result = await _cartService.DeleteCartAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}