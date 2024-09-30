using System.ComponentModel.DataAnnotations;

namespace GameReview.Models;
public class Notification
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public virtual Review? Review { get; set; }
    public string UserId { get; set; }
    public virtual User? User { get; set; }
    public string RelatedUserId { get; set; }
    public virtual User? RelatedUser { get; set; }
    public virtual Commentary? Commentary { get; set; }
    public virtual Follow? Follow { get; set; }
}