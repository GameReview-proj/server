namespace GameReview.DTOs.Commentary;

public record OutCommentaryDTO(
        int Id,
        string Comment,
        DateTime CreatedTime
    )
{ }