using GameReview.Models;

namespace GameReview.Data.Builders;

public interface IFollowBuilder
{
    IFollowBuilder SetFollower(User follower);
    IFollowBuilder SetFollowed(User followed);
    Follow Build();
}