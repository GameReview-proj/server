using System.ComponentModel.DataAnnotations;

namespace GameReview.DTOs.Commentary;

public record InCommentaryDTO(
        [Required]
        string Comment,
        int? ReviewId,
        int? CommentaryId
    )
{ }