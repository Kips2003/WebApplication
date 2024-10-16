using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models;

public class Order
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
    public int OrderProgressId { get; set; }
}