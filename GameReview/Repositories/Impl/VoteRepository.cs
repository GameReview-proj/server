using GameReview.Data;
using GameReview.Models;

namespace GameReview.Repositories.Impl;

public class VoteRepository(DatabaseContext context) : Repository<Vote>(context), IVoteRepository
{
    public Vote? GetByReviewIdCommentaryId(int? reviewId, int? commentaryId, string userId)
    {
        return _dbSet
            .FirstOrDefault(v =>
            (v.Commentary.Id.Equals(commentaryId)
            || v.Review.Id.Equals(reviewId))
            && v.User.Id.Equals(userId));
    }
}