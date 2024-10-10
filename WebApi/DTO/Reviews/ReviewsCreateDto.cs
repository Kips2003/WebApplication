namespace WebApi.DTO.Reviews;

public class ReviewsCreateDto
{
    public int ProductId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public string[] Images { get; set; }
    public int UserId { get; set; }
}