using Microsoft.EntityFrameworkCore;

namespace Deals;

public class DealContext : DbContext
{
    public DbSet<Store> Stores { get; set; }
    public DbSet<Deal> Deals { get; set; }

    public DealContext(DbContextOptions options) : base(options)
    {
        this.Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Store>()
            .HasMany(s => s.Deals)
            .WithOne(d => d.Store);
    }
}