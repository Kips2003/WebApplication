using WebApi.DTO.Cart;

namespace WebApi.Services.Cart;

public interface ICartService
{
    public Task<IEnumerable<CartDto>> GetCartAsync();
    public Task<IEnumerable<CartDto>> GetCartByUserIdAsync(int userid);
    public Task<CartDto> GetCartByIdAsync(int id);
    public Task<CartDto> CreateCartAsync(CartCreateDto cartCreateDto);
    public Task<CartDto> UpdateCartAsync(int id, CartCreateDto cartCreateDto);
    public Task<bool> DeleteCartAsync(int id);
}