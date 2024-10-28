using GameReview.Data;
using GameReview.Models;

namespace GameReview.Repositories.Impl;

public class CommentaryRepository(DatabaseContext context) : Repository<Commentary>(context), ICommentaryRepository
{
    public IEnumerable<Commentary> GetByReviewIdExternalIdUserId(int? reviewId, string? externalId, string? userId)
    {
        return _dbSet.Where(r =>
                 (!reviewId.HasValue || r.Review.Id.Equals(reviewId)) &&
                 (string.IsNullOrEmpty(externalId) || r.Review.ExternalId.Equals(externalId) &&
                 (string.IsNullOrEmpty(userId) || r.User.Id.Equals(userId)))
         );
    }
}