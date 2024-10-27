using GameReview.DTOs.User;

namespace GameReview.DTOs.Review;

public record OutReviewUserDTO(
        int Id,
        int Stars,
        string? Description,
        string ExternalId,
        DateTime CreatedDate,
        OutUserDTO User
    )
{ }