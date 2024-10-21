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

    public async Task<IEnumerable<CartDto>> GetCartAsync()
    {
        var carts = await _cart.GetCartAsync();

        return carts.Select(c => new CartDto
        {
            Id = c.Id,
            UserId = c.UserId,
            CartItems = c.CartItems.Select(ci => new CartItemDto
            {
                Id = ci.Id,
                CartId = ci.CartId,
                ProductId = ci.ProductId,
                Quantity = ci.Quantity
            }).Where(u => u.CartId == c.Id).ToList()
        }).ToList();
    }

    public async Task<CartDto> GetCartByUserIdAsync(int userid)
    {
        var carts = await _cart.GetCartByUserIdAsync(userid);
        
        return new CartDto
        {
            Id = carts.Id,
            UserId = carts.UserId,
            CartItems = carts.CartItems.Select(ci => new CartItemDto
            {
                Id = ci.Id,
                CartId = ci.CartId,
                ProductId = ci.ProductId,
                Quantity = ci.Quantity
            }).Where(u => u.CartId == carts.Id).ToList()
        };
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
        var cartByUserId = await _cart.GetCartByUserIdAsync(cartCreateDto.UserId);
        var cart = new Models.Cart
        {
            UserId = cartCreateDto.UserId,
            CartItems = cartCreateDto.CartItems.Select(ci => new CartItem
            {
                ProductId = ci.ProductId,
                Quantity = ci.Quantity,
            }).ToList()
        };

        if (cartByUserId is not null)
        {
            foreach (var cartItem in cart.CartItems)
            {
                var newCartITem = new CartItem();
                foreach (var cartItemDtoUser in cartByUserId.CartItems)
                {
                    if (cartItem.ProductId == cartItemDtoUser.ProductId)
                    {
                        cartItemDtoUser.Quantity += cartItem.Quantity;
                    }
                    else
                    {
                        newCartITem = new CartItem
                        {
                            ProductId = cartItem.ProductId,
                            Quantity = cartItem.Quantity,
                            CartId = cart.Id
                        };
                        cartByUserId.CartItems.Add(newCartITem);
                    }
                }
            }

            _cart.UpdateCartAsync(cartByUserId);
        }

        if (cartByUserId is null)
        {
            cart = await _cart.CreateCartAsync(cart);
        }
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