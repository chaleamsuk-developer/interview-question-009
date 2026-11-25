using ExampleCom.Interview.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleCom.Interview.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments => Set<Comment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>(entity =>
        {
            entity.Property(x => x.Title).HasMaxLength(150).IsRequired();
            entity.Property(x => x.AuthorName).HasMaxLength(100).IsRequired();
            entity.Property(x => x.ImageUrl).HasMaxLength(500);
            entity.HasMany(x => x.Comments).WithOne(x => x.Post)
                .HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(x => x.AuthorName).HasMaxLength(100).IsRequired();
            entity.Property(x => x.Message).HasMaxLength(1000).IsRequired();
            entity.HasIndex(x => new { x.PostId, x.CreatedAtUtc });
        });
    }
}
