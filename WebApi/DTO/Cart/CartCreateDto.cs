namespace WebApi.DTO.Cart;

public class CartCreateDto
{
    public int UserId { get; set; }
    public ICollection<CartItemCreateDto> CartItems { get; set; }
}