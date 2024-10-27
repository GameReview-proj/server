using GameReview.DTOs.Review;
using GameReview.Models;

namespace GameReview.Services;

public interface IReviewService : IWriteable<Review, InReviewDTO>,
    IReadable<Review>,
    IDeletable
{
    IEnumerable<Review> GetNewsPage(int from, int take);
    IEnumerable<Review> GetByUserIdExternalId(string? userId, string? externalId, int from, int take);
}