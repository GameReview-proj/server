using GameReview.Models;

namespace GameReview.Builders.Impl;

public class NotificationBuilder : INotificationBuilder
{
    private readonly Notification _notification;

    public NotificationBuilder()
    {
        _notification = new Notification()
        {
            CreatedDate = DateTime.Now,
            User = null
        };
    }

    public INotificationBuilder SetCommentary(Commentary commentary)
    {
        _notification.Commentary = commentary;
        
        return this;
    }

    public INotificationBuilder SetFollow(Follow follow)
    {
        _notification.Follow = follow;

        return this;
    }

    public INotificationBuilder SetUser(User user)
    {
        _notification.User = user;
        
        return this;
    }

    public INotificationBuilder SetVote(Vote vote)
    {
        _notification.Vote = vote;
        
        return this;
    }

    public Notification Build()
    {
        return _notification;
    }
}