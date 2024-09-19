using GameReview.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameReview.Data;

public class UserContext : IdentityDbContext<User>
{
    public UserContext(DbContextOptions opts) : base(opts) { }
}