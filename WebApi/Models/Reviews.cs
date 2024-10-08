using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models;

public class Reviews
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("Product")] 
    public int ProductId { get; set; }

    public Product Product { get; set; }

    public int Rating { get; set; }
    public string Comment { get; set; }
    public string[] Images { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}