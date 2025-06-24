using GameReview.Models;

namespace GameReview.Repositories;

public interface IVoteRepository : IRepository<Vote>
{
    Vote? GetByReviewIdCommentaryId(int? reviewId, int? commentaryId, string? userId);
}