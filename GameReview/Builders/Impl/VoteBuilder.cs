using GameReview.Models;

namespace GameReview.Builders.Impl;

public class VoteBuilder : IVoteBuilder
{
    private Vote _vote;

    public VoteBuilder()
    {
        _vote = new Vote()
        {
            User = null,
            CreatedDate = DateTime.Now
        };
    }

    public IVoteBuilder SetCommentary(Commentary commentary)
    {
        _vote.Commentary = commentary;
        
        return this;
    }

    public IVoteBuilder SetUser(User user)
    {
        _vote.User = user;

        return this;
    }

    public IVoteBuilder SetReview(Review review)
    {
        _vote.Review = review;

        return this;
    }

    public IVoteBuilder SetUp(bool up)
    {
        _vote.Up = up;

        return this;
    }

    public Vote Build()
    {
        return _vote;
    }

    public IVoteBuilder ReBuild(Vote vote)
    {
        _vote = vote;

        return this;
    }
}