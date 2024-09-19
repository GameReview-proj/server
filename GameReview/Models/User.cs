using Microsoft.AspNetCore.Identity;

namespace GameReview.Models;

public class User : IdentityUser
{
    public string? Picture { get; set; }
    public string? SteamId { get; set; }

    public User() : base() { }
}