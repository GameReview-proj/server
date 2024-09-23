﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameReview.Models;

public class Review()
{
    [Key]
    public int Id { get; set; }
    public int Stars { get; set; }
    public string? Description { get; set; }
    public required string ExternalId { get; set; }
    public virtual User? User { get; set; }
}