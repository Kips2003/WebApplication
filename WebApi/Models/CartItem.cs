using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models;

public class CartItem
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("Cart")]
    public int CartId { get; set; }
    public Cart Cart { get; set; }
    
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}