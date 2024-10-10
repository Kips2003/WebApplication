using WebApi.DTO;
using WebApi.DTO.Reviews;
using WebApi.Services.Authentication;

namespace WebApi.Services.Reviews;

public class ReviewsService : IReviewsService
{
    private readonly IReviewsRepository _reviews;
    private readonly IUserRepository _user;

    public ReviewsService(IReviewsRepository reviews, IUserRepository user)
    {
        _reviews = reviews;
        _user = user;
    }
    public async Task<IEnumerable<ReviewsDto>> GetReviewsAsync()
    {
        var reviews = await _reviews.GetReviewsAsync();
        return reviews.Select(r => new ReviewsDto
        {
            Id = r.Id,
            ProductId = r.ProductId,
            Rating = r.Rating,
            Comment = r.Comment,
            Images = r.Images,
            UserId = r.UserId
        }).ToList();
    }

    public async Task<ReviewsDto> GetReviewsByIdAsync(int id)
    {
        var review = await _reviews.GetReviewsByIdAsync(id);
        if (review is null)
            return null;

        return new ReviewsDto
        {
            Id = review.Id,
            ProductId = review.ProductId,
            Rating = review.Rating,
            Comment = review.Comment,
            Images = review.Images,
            UserId = review.UserId
        };
    }

    public async Task<IEnumerable<ReviewsDto>> GetReviewsByProductIdAsync(int productId)
    {
        var reviews = await _reviews.GetReviewsByProdyctIdAsync(productId);
        return reviews.Select(r => new ReviewsDto
        {
            Id = r.Id,
            ProductId = r.ProductId,
            Rating = r.Rating,
            Comment = r.Comment,
            Images = r.Images,
            UserId = r.UserId
        }).ToList();
    }

    public async Task<ReviewsDto> CreateReviewAsync(ReviewsCreateDto reviews)
    {
        var review = new Models.Reviews
        {
            ProductId = reviews.ProductId,
            Rating = reviews.Rating,
            Comment = reviews.Comment,
            Images = reviews.Images,
            UserId = reviews.UserId
        };

        review = await _reviews.CreateReviewAsync(review);
        
        return new ReviewsDto
        {
            Id = review.Id,
            ProductId = review.ProductId,
            Rating = review.Rating,
            Comment = review.Comment,
            Images = review.Images,
            UserId = review.UserId
        };
    }

    public async Task<AuthResponseDto> UpdateReviewAsync(int UserId, ReviewsDto reviews)
    {
        var review = await _reviews.GetReviewsByIdAsync(reviews.Id);
        if (review is null)
            return new AuthResponseDto { Success = false, Message = "This review does not exist" };

        var user = await _user.getUserById(UserId);
        if (user is null)
            return new AuthResponseDto { Success = false, Message = "User does now exist" };

        if (reviews.UserId != UserId && user.PrivilageId != 1 && user.PrivilageId != 2)
        {
            return new AuthResponseDto { Success = false, Message = "Youdo not have access to change this address" };
        }

        review.ProductId = reviews.ProductId;
        review.Rating = reviews.Rating;
        review.Comment = reviews.Comment;
        review.Images = reviews.Images;

        await _reviews.UpdateReviewAsync(review);

        return new AuthResponseDto { Success = true, Message = "Review updated successfully" };
    }

    public async Task<AuthResponseDto> DeleteReviewAsync(int UserId, int id)
    {
        if (id == null)
            return new AuthResponseDto { Success = false, Message = "Provided Number is null" };

        var review = await _reviews.GetReviewsByIdAsync(id);
        if (review is null)
            return new AuthResponseDto { Success = false, Message = "There is no review on this ID" };
        
        var user = await _user.getUserById(UserId);
        if (user is null)
            return new AuthResponseDto { Success = false, Message = "User does now exist" };
        
        if (review.UserId != UserId && user.PrivilageId != 1 && user.PrivilageId != 2)
        {
            return new AuthResponseDto { Success = false, Message = "Youdo not have access to change this address" };
        }
        
        await _reviews.DeleteReviewAsync(id);

        return new AuthResponseDto { Success = true, Message = "Review Removed from the database" };    
    }
}