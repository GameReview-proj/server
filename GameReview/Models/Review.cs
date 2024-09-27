using System.ComponentModel.DataAnnotations;

namespace GameReview.Models;

public class Review()
{
    [Key]
    public int Id { get; set; }
    public int Stars { get; set; }
    public string? Description { get; set; }
    public required string ExternalId { get; set; }
    public DateTime CreatedDate { get; set; }
    public virtual User? User { get; set; }
    public virtual IList<Commentary> Commentaries { get; } = new List<Commentary>();
}