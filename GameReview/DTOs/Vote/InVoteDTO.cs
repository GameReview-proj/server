namespace GameReview.DTOs.Vote;

public record InVoteDTO(
        int? ReviewId,
        int? CommentaryId,
        bool Up
    )
{ }