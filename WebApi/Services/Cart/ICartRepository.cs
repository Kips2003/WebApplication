namespace WebApi.Services.Cart;

public interface ICartRepository
{
    public Task<IEnumerable<Models.Cart>> GetCartAsync();
    public Task<IEnumerable<Models.Cart>> GetCartByUserIdAsync(int userId);
    public Task<Models.Cart> GetCartByIdAsync(int id);
    public Task<Models.Cart> CreateCartAsync(Models.Cart cart);
    public Task<Models.Cart> UpdateCartAsync(Models.Cart cart);
    public Task<bool> DeleteCartAsync(int id);
}