using System.ComponentModel.DataAnnotations;

namespace GameReview.Data.DTOs.Commentary;

public record InCommentaryDTO(
        [Required]
        string Comment,
        int? ReviewId,
        int? CommentaryId
    )
{}