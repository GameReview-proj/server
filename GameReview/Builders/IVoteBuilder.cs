using GameReview.Models;

namespace GameReview.Builders;

public interface IVoteBuilder
{
    public IVoteBuilder SetUp(bool up);
    public IVoteBuilder SetReview(Review review);
    public IVoteBuilder SetCommentary(Commentary commentary);
    public IVoteBuilder SetUser(User user);
    public IVoteBuilder ReBuild(Vote vote);
    public Vote Build();
}