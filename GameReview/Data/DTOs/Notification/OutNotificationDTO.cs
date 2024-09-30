namespace GameReview.Data.DTOs.Notification;

public record OutNotificationDTO (
        DateTime CreatedDate,
        string Description,
        string UserId,
        string RelatedUserUsername
    )
{

}