using WebApi.Models;

namespace WebApi.DTO.Product;

public class ProductCreateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int Stock { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public decimal Width { get; set; }
    public decimal Depth { get; set; }
    public string QrCode { get; set; }
    public string BarCode { get; set; }
    public string[] Images { get; set; }
    public string Thumbnail { get; set; }
}