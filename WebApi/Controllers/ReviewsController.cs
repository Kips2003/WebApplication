using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.Reviews;
using WebApi.Services.Reviews;

namespace WebApi.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewsService _reviews;

    public ReviewsController(IReviewsService reviews)
    {
        _reviews = reviews;
    }

    [HttpGet]
    public async Task<IActionResult> GetReviewsAsync()
    {
        var reviews = await _reviews.GetReviewsAsync();

        return Ok(reviews);
    }
    
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetReviewByIdAsync(int id)
    {
        var review = await _reviews.GetReviewsByIdAsync(id);
        if (review is null)
            return NotFound();

        return Ok(review);
    }

    [HttpGet("product/{productId}")]
    public async Task<IActionResult> GetReviewsByProductIdAsync(int productId)
    {
        var reviews = await _reviews.GetReviewsByProductIdAsync(productId);

        return Ok(reviews);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReviewAsync(ReviewsCreateDto request)
    {
        var review = await _reviews.CreateReviewAsync(request);
        return CreatedAtAction(nameof(GetReviewByIdAsync), new {id = review.Id}, review);
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateReviewAsync(int userId, ReviewsDto reviewsDto)
    {
        var review = await _reviews.UpdateReviewAsync(userId, reviewsDto);

        if (!review.Success)
            return BadRequest(review.Message);

        return Ok(review.Message);
    }

    [HttpDelete("{userId}/{id}")]
    public async Task<IActionResult> DeleteReviewAsync(int userId, int id)
    {
        var result = await _reviews.DeleteReviewAsync(userId, id);
        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result.Message);
    }
}