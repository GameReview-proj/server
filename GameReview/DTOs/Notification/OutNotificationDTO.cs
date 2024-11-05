using GameReview.DTOs.Commentary;
using GameReview.DTOs.Review;
using GameReview.DTOs.User;
using System.Text.Json.Serialization;
using GameReview.DTOs.Vote;
using GameReview.Models;
using System.Collections.Specialized;
using GameReview.DTOs.Follow;

namespace GameReview.DTOs.Notification;

public class OutNotificationDTO
{
    public DateTime CreatedDate { get; set; }

    public OutUserDTO User { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public OutReviewDTO? Review { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public OutCommentaryDTO? Commentary { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public OutVoteDTO? Vote { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public OutFollowUsersDTO? Follow { get; set; }

    [JsonConstructor]
    public OutNotificationDTO(DateTime createdDate, OutUserDTO user, OutReviewDTO review, OutCommentaryDTO commentary, OutVoteDTO vote)
    {
        CreatedDate = createdDate;
        User = user;
        Review = review;
        Commentary = commentary;
        Vote = vote;
    }

    public OutNotificationDTO(Models.Notification notification)
    {
        CreatedDate = notification.CreatedDate;
        User = new OutUserDTO(notification.User);

        if (notification.Review is not null)
        {
            Review = new OutReviewDTO(notification.Review.Id,
                notification.Review.Stars,
                notification.Review.Description,
                notification.Review.ExternalId,
                notification.Review.CreatedDate);
        }

        if (notification.Commentary is not null)
        {
            Commentary = new OutCommentaryDTO(notification.Commentary.Id,
            notification.Commentary.Comment,
            notification.Commentary.CreatedDate);
        }

        if (notification.Vote is not null)
        {
            Vote = new OutVoteDTO(notification.Vote.Id,
            notification.Vote.Up,
            notification.Vote.CreatedDate);
        }

        if (notification.Follow is not null)
        {
            Follow = new(new OutUserDTO(notification.Follow.Follower),
                new OutUserDTO(notification.Follow.Followed),
                notification.Follow.CreatedDate);
        }
    }
}