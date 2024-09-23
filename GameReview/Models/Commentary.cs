using System.ComponentModel.DataAnnotations;

namespace GameReview.Models;

public class Commentary
{
    [Key]
    public int Id { get; set; }
    public required string Comment { get; set; }
    public DateTime CreatedDate { get; set; }
    public virtual User? User { get; set; }
    public virtual Review? Review { get; set; }
    public virtual Commentary? LinkedCommentary { get; set; }
}