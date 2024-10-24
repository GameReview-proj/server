using System.ComponentModel.DataAnnotations;

namespace GameReview.Models;

public class Follow
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string FollowerId { get; set; }
    public virtual User? Follower { get; set; }
    public string FollowedId { get; set; }
    public virtual User? Followed { get; set; }
    public virtual List<Notification> Notifications { get; set; } = [];
}