using GameReview.Models;

namespace GameReview.Builders;

public interface INotificationBuilder
{
    public INotificationBuilder SetUser(User user);
    public INotificationBuilder SetRelatedUser(User relatedUser);
    public INotificationBuilder SetCommentary(Commentary commentary);
    public INotificationBuilder SetFollow(Follow follow);

}