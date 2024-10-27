using GameReview.Builders;
using GameReview.Models;

namespace GameReview.Builders.Impl;

public class ReviewBuilder : IReviewBuilder
{
    private readonly Review _review;

    public ReviewBuilder()
    {
        _review = new()
        {
            CreatedDate = DateTime.Now
        };
    }

    public IReviewBuilder SetDescription(string description)
    {
        _review.Description = description;
        return this;
    }

    public IReviewBuilder SetExternalId(string externalId)
    {
        _review.ExternalId = externalId;
        return this;
    }

    public IReviewBuilder SetStars(int stars)
    {
        _review.Stars = stars;
        return this;
    }

    public IReviewBuilder SetUser(User user)
    {
        _review.User = user;
        return this;
    }

    public Review Build()
    {
        return _review;
    }
}