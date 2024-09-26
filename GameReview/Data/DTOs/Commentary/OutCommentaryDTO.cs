namespace GameReview.Data.DTOs.Commentary;

public record OutCommentaryDTO(
        int Id,
        string Comment,
        DateTime CreatedTime
    )
{ }