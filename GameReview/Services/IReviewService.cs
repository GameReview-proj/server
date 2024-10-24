using GameReview.Data.DTOs.Review;
using GameReview.Models;

namespace GameReview.Services;

public interface IReviewService : IWriteable<Review, InReviewDTO>,
    IReadable<Review>,
    IDeletable
{
    IEnumerable<Review> GetNewsPage(int from, int take);
}