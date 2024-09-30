using GameReview.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace GameReview.Data;

public class DatabaseContext : IdentityDbContext<User>
{
    public DatabaseContext(DbContextOptions opts) : base(opts) { }

    public DbSet<Review> Reviews { get; set; }
    public DbSet<Commentary> Commentaries { get; set; }
    public DbSet<Follow> Follows { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Follow>()
            .HasOne(f => f.Follower)
            .WithMany(u => u.Followers)
            .HasForeignKey("FollowerId");

        builder.Entity<Follow>()
            .HasOne(f => f.Following)
            .WithMany(u => u.Following)
            .HasForeignKey("FollowingId");

        builder.Entity<Notification>()
            .HasOne(n => n.RelatedUser)
            .WithMany(u => u.RelatedNotifications)
            .HasForeignKey("RelatedUserId");

        builder.Entity<Notification>()
            .HasOne(n => n.User)
            .WithMany(u => u.Notifications)
            .HasForeignKey("UserId");

        base.OnModelCreating(builder);
    }
}