namespace GameReview.DTOs.Vote;

public record OutVoteDTO(
        int Id,
        bool Up,
        DateTime CreatedDate
    )
{
}