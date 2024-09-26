using System.ComponentModel.DataAnnotations;
using GameReview.Services.Annotations;

namespace GameReview.Data.DTOs.Commentary;

public record InCommentaryDTO(
        [Required]
        string Comment,
        [Required]
        string UserId,
        int? ReviewId,
        int? CommentaryId
    )
{}