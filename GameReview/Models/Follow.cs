using System.ComponentModel.DataAnnotations;

namespace GameReview.Models;

public class Follow
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string FollowerId { get; set; }
    public virtual User? Follower { get; set; }
    public string FollowingId { get; set; }
    public virtual User? Following { get; set; }
}