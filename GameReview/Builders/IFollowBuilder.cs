using GameReview.Models;

namespace GameReview.Builders;

public interface IFollowBuilder
{
    IFollowBuilder SetFollower(User follower);
    IFollowBuilder SetFollowed(User followed);
    Follow Build();
}