﻿using System.ComponentModel.DataAnnotations;

namespace GameReview.Models;

public class Review()
{
    [Key]
    public int Id { get; set; }
    public int Stars { get; set; }
    public string? Description { get; set; }
    public string ExternalId { get; set; }
    public DateTime CreatedDate { get; set; }
    public virtual User? User { get; set; }
    public virtual List<Notification> Notifications { get; } = [];
    public virtual List<Commentary>? Commentaries { get; } = [];
    public virtual List<Vote>? Votes { get; } = [];
}