using GameReview.Data.DTOs.Notification;
using GameReview.Models;

namespace GameReview.Data.Adapters;

public static class NotificationAdapter
{
    public static OutNotificationDTO ToNotificationDTO(Notification notification)
    {
        return new(
                notification.CreatedDate,
                "",
                notification.User.Id,
                notification.RelatedUser.UserName
            );
    }
}