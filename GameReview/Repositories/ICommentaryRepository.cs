using GameReview.Models;

namespace GameReview.Repositories;

public interface ICommentaryRepository : IRepository<Commentary>
{
    IEnumerable<Commentary> GetByReviewIdExternalIdUserId(int? reviewId, string? externalId, string? userId);
}