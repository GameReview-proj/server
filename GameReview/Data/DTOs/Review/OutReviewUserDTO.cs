using GameReview.Data.DTOs.User;

namespace GameReview.Data.DTOs.Review;

public record OutReviewUserDTO(
        int Id,
        int Stars,
        string? Description,
        string ExternalId,
        DateTime CreatedDate,
        OutUserDTO User
    )
{ }