using GameReview.Builders;
using GameReview.Models;

namespace GameReview.Builders.Impl;

public class CommentaryBuilder : ICommentaryBuilder
{
    private readonly Commentary _commentary;

    public CommentaryBuilder()
    {
        _commentary = new Commentary
        {
            CreatedDate = DateTime.Now
        };
    }

    public ICommentaryBuilder SetComment(string comment)
    {
        _commentary.Comment = comment;
        return this;
    }

    public ICommentaryBuilder SetLink(Commentary? commentary, Review? review)
    {
        if (commentary is not null) _commentary.LinkedCommentary = commentary;
        if (review is not null) _commentary.Review = review;

        return this;
    }

    public ICommentaryBuilder SetLinkedCommentary(Commentary commentary)
    {
        _commentary.LinkedCommentary = commentary;
        return this;
    }

    public ICommentaryBuilder SetReview(Review review)
    {
        _commentary.Review = review;
        return this;
    }

    public ICommentaryBuilder SetUser(User user)
    {
        _commentary.User = user;
        return this;
    }

    public Commentary Build()
    {
        return _commentary;
    }
}