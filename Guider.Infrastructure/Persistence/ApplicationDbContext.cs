using Guider.Domain.Categories;
using Guider.Domain.Tags;
using Guider.Domain.Venues;
using Microsoft.EntityFrameworkCore;

namespace Guider.Infrastructure.Persistence;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<Venue> Venues { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}