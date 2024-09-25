using Org.BouncyCastle.Crypto.Engines;

namespace WebApi.Services.Reviews;

public interface IReviewsRepository
{
    public Task<IEnumerable<Models.Reviews>> GetReviewsAsync();
    public Task<Models.Reviews> GetReviewsByIdAsync(int id);
    public Task<Models.Reviews> GetReviewsByProdyctIdAsync(int productId);
    public Task<Models.Reviews> CreateReviewAsync(Models.Reviews reviews);
    public Task<Models.Reviews> UpdateReviewAsync(int userId, int productId);
}