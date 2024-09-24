
using GameReview.Data.DTOs.Commentary;
using GameReview.Models;

namespace GameReview.Data.Adapters;

public static class CommentaryAdapter
{
    public static Commentary ToEntity(InCommentaryDTO dto, User user, Commentary? commentary, Review? review)
    {
        if (commentary != null && review != null)
            throw new ApplicationException("Um comentário só pode ser atribuído a uma avaliação OU comentário");

        return new()
        {
            Comment = dto.Comment,
            User = user,
            LinkedCommentary = commentary,
            Review = review,
            CreatedDate = DateTime.Now
        };
    }

    public static OutCommentaryDTO ToCommentaryDTO(Commentary commentary)
    {
        return new(
            commentary.Id,
            commentary.Comment,
            commentary.CreatedDate
        );
    }
}