using GameReview.Models;

namespace GameReview.Data.Builders.Impl;

public class FollowBuilder : IFollowBuilder
{
    private readonly Follow _follow;

    public FollowBuilder()
    {
        _follow = new Follow()
        {
            CreatedDate = DateTime.Now
        };
    }

    public IFollowBuilder SetFollowed(User followed)
    {
        _follow.Followed = followed;
        return this;
    }

    public IFollowBuilder SetFollower(User follower)
    {
        _follow.Follower = follower;
        return this;
    }

    public Follow Build()
    {
        return _follow;
    }
}