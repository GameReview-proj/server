using GameReview.Data.DTOs.Review;
using GameReview.Models;

namespace GameReview.Data.Adapters;

public static class ReviewAdapter
{
    public static Review ToEntity(InReviewDTO dto, User user)
    {
        return new()
        {
            Description = dto.Description,
            Stars = dto.Stars,
            ExternalId = dto.ExternalId,
            User = user
        };
    }
}