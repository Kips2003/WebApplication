namespace WebApi.DTO.Product;

public class ProductSearchDto
{
    public string title { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public int? CategoryId { get; set; }
}