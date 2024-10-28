using GameReview.Data;
using GameReview.Models;

namespace GameReview.Repositories.Impl;

public class ReviewRepository(DatabaseContext context) : Repository<Review>(context), IReviewRepository
{
    public IEnumerable<Review> GetByUserIdExternalId(string? userId, string? externalId, int from, int take)
    {
        return _dbSet.Where(r =>
            (string.IsNullOrEmpty(userId) || r.User.Id == userId) &&
            (string.IsNullOrEmpty(externalId) || r.ExternalId == externalId))
            .Skip(from)
            .Take(take);
    }

    public IEnumerable<Review> GetNewsPageable(int from, int take)
    {
        return _dbSet
            .OrderBy(r => r.CreatedDate)
            .Skip(from)
            .Take(take);
    }
}