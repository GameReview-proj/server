using System.ComponentModel.DataAnnotations;

namespace GameReview.DTOs.Review;

public record InReviewDTO(
    [Range(0, 5)]
    [Required]
    int Stars,
    [MaxLength(255)]
    string? Description,
    [Required]
    string ExternalId
    )
{ }