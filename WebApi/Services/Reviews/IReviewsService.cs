using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Org.BouncyCastle.Crypto.Engines;
using WebApi.DTO;
using WebApi.DTO.Reviews;

namespace WebApi.Services.Reviews;

public interface IReviewsService
{
    Task<IEnumerable<ReviewsDto>> GetReviewsAsync();
    Task<ReviewsDto> GetReviewsByIdAsync(int id);
    Task<IEnumerable<ReviewsDto>> GetReviewsByProductIdAsync(int productId);
    Task<ReviewsDto> CreateReviewAsync(ReviewsCreateDto reviews);
    Task<AuthResponseDto> UpdateReviewAsync(int UserId, ReviewsDto reviews);
    Task<AuthResponseDto> DeleteReviewAsync(int userId, int id);
}