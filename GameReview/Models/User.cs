﻿using Microsoft.AspNetCore.Identity;

namespace GameReview.Models;

public class User : IdentityUser
{
    public string? Picture { get; set; }
    public string? SteamId { get; set; }
    public DateTime CreatedDate { get; set; }
    public virtual List<Review>? Reviews { get; set; }

    public User() : base() { }
}