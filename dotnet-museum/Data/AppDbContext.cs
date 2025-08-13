using dotnet_museum.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_museum.Data;

public class AppDbContext : DbContext
{
    public DbSet<Artifact> Artifacts { get; set; }
    public DbSet<Gallery> Galleries { get; set; }
    public DbSet<Employee> Employees { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artifact>()
            .HasOne(e => e.Gallery)
            .WithMany(d => d.Artifacts)
            .HasForeignKey(e => e.GalleryId);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // no connection string until we add a migration and update the database        
        optionsBuilder.UseSqlServer(
        );
    }
    
}