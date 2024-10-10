using Microsoft.EntityFrameworkCore;
using WebApi.Data;

namespace WebApi.Services.Reviews;

public class ReviewsRepository : IReviewsRepository
{
    private readonly DataContext _data;

    public ReviewsRepository(DataContext data)
    {
        _data = data;
    }
    public async Task<IEnumerable<Models.Reviews>> GetReviewsAsync()
    {
        return await _data.Reviews.ToListAsync();
    }

    public async Task<Models.Reviews> GetReviewsByIdAsync(int id)
    {
        return await _data.Reviews.FindAsync(id);
    }

    public async Task<IEnumerable<Models.Reviews>> GetReviewsByProdyctIdAsync(int productId)
    {
        var product =await _data.Reviews.Where(r => r.ProductId == productId).ToListAsync();
        return product;
    }

    public async Task<Models.Reviews> CreateReviewAsync(Models.Reviews reviews)
    {
        _data.Reviews.Add(reviews);
        await _data.SaveChangesAsync();
        return reviews;
    }

    public async Task<Models.Reviews> UpdateReviewAsync(Models.Reviews review)
    {
        _data.Entry(review).State = EntityState.Modified;
        await _data.SaveChangesAsync();
        return review;
    }

    public async Task<bool> DeleteReviewAsync(int id)
    {
        var review = await _data.Reviews.FindAsync(id);
        if (review is null)
            return false;

        _data.Reviews.Remove(review);
        await _data.SaveChangesAsync();
        return true;
    }
}