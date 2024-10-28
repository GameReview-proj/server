using GameReview.Models;

namespace GameReview.Repositories;

public interface IReviewRepository : IRepository<Review>
{
    IEnumerable<Review> GetByUserIdExternalId(string? userId, string? externalId, int from, int take);
    IEnumerable<Review> GetNewsPageable(int from, int take);
}