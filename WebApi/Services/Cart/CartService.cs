using WebApi.DTO.Cart;
using WebApi.Models;

namespace WebApi.Services.Cart;

public class CartService : ICartService
{
    private readonly ICartRepository _cart;

    public CartService(ICartRepository cart)
    {
        _cart = cart;
    }
    
    public async Task<CartDto> GetCartByIdAsync(int id)
    {
        var cart = await _cart.GetCartByIdAsync(id);
        if (cart is null)
            return null;

        return new CartDto
        {
            Id = cart.Id,
            UserId = cart.UserId,
            CartItems = cart.CartItems.Select(ci => new CartItemDto
            {
                Id = ci.Id,
                CartId = ci.CartId,
                ProductId = ci.ProductId,
                Quantity = ci.Quantity
            }).ToList()
        };
    }

    public async Task<CartDto> CreateCartAsync(CartCreateDto cartCreateDto)
    {
        var cart = new Models.Cart
        {
            UserId = cartCreateDto.UserId,
            CartItems = cartCreateDto.CartItems.Select(ci => new CartItem
            {
                ProductId = ci.ProductId,
                Quantity = ci.Quantity,
            }).ToList()
        };

        cart = await _cart.CreateCartAsync(cart);

        return new CartDto
        {
            Id = cart.Id,
            UserId = cart.UserId,
            CartItems = cart.CartItems.Select(ci => new CartItemDto
            {
                Id = ci.Id,
                CartId = ci.CartId,
                ProductId = ci.ProductId,
                Quantity = ci.Quantity
            }).ToList()
        };
    }

    public async Task<CartDto> UpdateCartAsync(int id, CartCreateDto cartCreateDto)
    {
        var cart = await _cart.GetCartByIdAsync(id);
        if (cart is null)
            return null;

        cart.UserId = cartCreateDto.UserId;
        cart.CartItems = cartCreateDto.CartItems.Select(ci => new CartItem
        {
            ProductId = ci.ProductId,
            Quantity = ci.Quantity
        }).ToList();

        cart = await _cart.UpdateCartAsync(cart);

        return new CartDto
        {
            Id = cart.Id,
            UserId = cart.UserId,
            CartItems = cart.CartItems.Select(ci => new CartItemDto
            {
                Id = ci.Id,
                CartId = ci.CartId,
                ProductId = ci.ProductId,
                Quantity = ci.Quantity
            }).ToList()
        };
    }

    public async Task<bool> DeleteCartAsync(int id)
    {
        return await _cart.DeleteCartAsync(id);
    }
}