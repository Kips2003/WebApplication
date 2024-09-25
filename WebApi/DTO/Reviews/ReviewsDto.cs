namespace WebApi.DTO.Reviews;

public class ReviewsDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    
    public int Rating { get; set; }
    public string Comment { get; set; }
    public string[] Images { get; set; }
}