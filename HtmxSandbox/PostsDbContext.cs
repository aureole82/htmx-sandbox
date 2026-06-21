using HtmxSandbox.Models;
using Microsoft.EntityFrameworkCore;

namespace HtmxSandbox;

public class PostsDbContext(DbContextOptions<PostsDbContext> options) : DbContext(options)
{
    public required DbSet<DbPost> Posts { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DbPost>()
            .Property(post => post.Created)
            .HasDefaultValueSql("GETDATE()")
            ;
    }
}