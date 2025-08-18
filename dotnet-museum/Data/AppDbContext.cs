using System;
using dotnet_museum.Models;
using dotnet_museum.Models.Booking;
using dotnet_museum.Models.MuseumEvents;
using dotnet_museum.Models.TourismCompany;
using Microsoft.EntityFrameworkCore;

namespace dotnet_museum.Data;

public class AppDbContext : DbContext
{
    public DbSet<Artifact> Artifacts { get; set; }
    public DbSet<Gallery> Galleries { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EventModel> Events { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<BookingModel> Bookings { get; set; }
    public DbSet<Company> Companies { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artifact>()
            .HasOne(e => e.Gallery)
            .WithMany(d => d.Artifacts)
            .HasForeignKey(e => e.GalleryId);
        
        modelBuilder.Entity<Artifact>(entity =>
        {
            entity.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(2000);

            entity.Property(a => a.GalleryId)
                .IsRequired();

            entity.Property(a => a.Origin)
                .HasMaxLength(100);

            entity.Property(a => a.Period)
                .HasMaxLength(100);

            entity.Property(a => a.Civilization)
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Gallery>(entity =>
        {
            entity.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(g => g.Description)
                .IsRequired()
                .HasMaxLength(2000);
            
            entity.Property(g => g.GalleryId)
                .IsRequired();
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Department)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Salary)
                .IsRequired();
        });
        
        
        modelBuilder.Entity<EventModel>()
            .HasOne(e => e.Category)
            .WithMany(c => c.Events)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<EventModel>()
            .HasOne(e => e.Gallery)
            .WithMany(g => g.Events)
            .HasForeignKey(e => e.GalleryId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<BookingModel>()
            .HasOne(b => b.Event)
            .WithMany(e => e.Bookings)
            .HasForeignKey(b => b.EventId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<BookingModel>()
            .HasOne(b => b.TourismCompany)
            .WithMany(c => c.Bookings)
            .HasForeignKey(b => b.TourismCompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<EventModel>(entity =>
        {
            entity.HasKey(e => e.EventId);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(2000);
            
            entity.Property(e => e.Capacity)
                .HasDefaultValue(10);

            entity.Property(e => e.TicketPrice)
                .IsRequired()
                .HasPrecision(10, 2);

            entity.Property(e => e.ImagePath)
                .IsRequired();
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.ToTable("Categories");
        });

        modelBuilder.Entity<BookingModel>(entity =>
        {
            entity.HasKey(e => e.BookingId);
            entity.Property(e => e.CustomerName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.CustomerPhone)
                .IsRequired()
                .HasMaxLength(16);

            entity.Property(e => e.NumberOfTickets)
                .HasDefaultValue(1);

            entity.Property(e => e.TotalPrice)
                .HasPrecision(15, 2);
            
            entity.Property(e => e.ReservationType)
                .HasDefaultValue(ReservationType.Regular);
        });

        modelBuilder.Entity<Company>(entity =>
        {
            
            entity.Property(e => e.RegistrationNumber)
                .IsRequired();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.DiscountPercentage)
                .HasPrecision(5, 2);
        });

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // no connection string until we add a migration and update the database        
        optionsBuilder.UseSqlServer(
            "Server=.\\SQLEXPRESS;Database=museum;Integrated Security=True;Encrypt=False;"
            
        );


    }
    
}