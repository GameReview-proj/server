using System.ComponentModel.DataAnnotations;

namespace GameReview.Models;

public class Vote
{
    [Key]
    public int Id { get; set; }
    public bool Up { get; set; }
    public DateTime CreatedDate { get; set; }
    public virtual Review? Review { get; set; }
    public virtual Commentary? Commentary { get; set; }
}