using GameReview.Models;

namespace GameReview.Builders;

public interface ICommentaryBuilder
{
    ICommentaryBuilder SetComment(string comment);
    ICommentaryBuilder SetUser(User user);
    ICommentaryBuilder SetReview(Review review);
    ICommentaryBuilder SetLinkedCommentary(Commentary commentary);
    ICommentaryBuilder SetLink(Commentary? commentary, Review? review);
    Commentary Build();
}