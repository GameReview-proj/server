﻿using System.ComponentModel.DataAnnotations;

namespace GameReview.Models;
public class Notification
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public virtual Boolean Readed { get; set; }
    public virtual required User User { get; set; }
    public virtual Review? Review { get; set; }
    public virtual Commentary? Commentary { get; set; }
    public virtual Follow? Follow { get; set; }
    public virtual Vote? Vote { get; set; }
}