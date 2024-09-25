namespace WebApi.DTO.Reviews;

public class ReviewsCreateDto
{
    public int ProductId { get; set; }
    public int Ratin { get; set; }
    public string Comment { get; set; }
    public string[] Images { get; set; }
}