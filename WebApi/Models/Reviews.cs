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
    public ICollection<string> Images { get; set; }
}