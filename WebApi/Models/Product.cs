using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    
    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<string> Tags { get; set; }
    public decimal Weight { get; set; }
    public decimal Width { get; set; }
    public decimal Depth { get; set; }
    public decimal Height { get; set; }
    public ICollection<Reviews> Reviews { get; set; }
    public ICollection<string> Images { get; set; }
    public string Thumbnail { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string BarCode { get; set; }
    public string QrCode { get; set; }
}