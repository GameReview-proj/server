namespace GameReview.DTOs.Review;
public record OutReviewDTO(int Id,
    int Stars,
    string? Description,
    string ExternalId,
    DateTime CreatedDate)
{ }