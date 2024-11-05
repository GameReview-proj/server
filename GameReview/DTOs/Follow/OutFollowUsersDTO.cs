using GameReview.DTOs.User;

namespace GameReview.DTOs.Follow;

public record OutFollowUsersDTO(
        OutUserDTO Follower,
        OutUserDTO Followed,
        DateTime CreatedDate
    )
{ }