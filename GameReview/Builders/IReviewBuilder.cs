using GameReview.Models;

namespace GameReview.Builders;

public interface IReviewBuilder
{
    IReviewBuilder SetDescription(string description);
    IReviewBuilder SetStars(int stars);
    IReviewBuilder SetExternalId(string externalId);
    IReviewBuilder SetUser(User user);
    Review Build();
}