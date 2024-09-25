using System.Collections;
using System.Collections.ObjectModel;
using Microsoft.VisualBasic;
using WebApi.Models;

namespace WebApi.DTO.Product;

public class ProductDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int Stock { get; set; }
    public string[] Tags { get; set; }
    public decimal Weight { get; set; }
    public decimal Width { get; set; }
    public decimal Depth { get; set; }
    public decimal Height { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string QrCode { get; set; }
    public string BarCode { get; set; }
    public ICollection<Models.Reviews> Reviews { get; set; }
    public ICollection<string> Images { get; set; }
    public string Thumbnail { get; set; }
}