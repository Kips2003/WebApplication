namespace WebApi.DTO.Order;

public class OrderDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public ICollection<OrderItemDto> OrderItems { get; set; }
}