using GameReview.Models;

namespace GameReview.Data.Adapters;

public static class FollowAdapter
{
    public static Follow ToEntity(User follower, User followed)
    {
        return new Follow
        {
            Follower = follower,
            Followed = followed,
            CreatedDate = DateTime.Now
        };
    }
}