using GameReview.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameReview.Data;

public class DatabaseContext : IdentityDbContext<User>
{
    public DatabaseContext(DbContextOptions opts) : base(opts) { }

    public DbSet<Review> Reviews { get; set; }
    public DbSet<Commentary> Commentaries { get; set; }
}