using dotnet_museum.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_museum.Data;

public class AppDbContext : DbContext
{
    public DbSet<Artifact> Artifacts { get; set; }
    public DbSet<Gallery> Galleries { get; set; }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=DotnetMuseum;Trusted_Connection=True;"
        );
    }
    
}