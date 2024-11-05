using GameReview.Models;

namespace GameReview.Builders;

public interface INotificationBuilder
{
    public INotificationBuilder SetUser(User user);
    public INotificationBuilder SetCommentary(Commentary commentary);
    public INotificationBuilder SetFollow(Follow follow);
    public INotificationBuilder SetVote(Vote vote);
    public Notification Build();
}