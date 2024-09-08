using AuthSystem.Areas.Identity.Data;
using AuthSystem.DbModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthSystem.Data;

public class AuthDbContext : IdentityDbContext<ApplicationUser>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
    }

    public DbSet<CustomerInfo> CustomerInfos { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Gender> Genders { get; set; }
    //protected override void OnModelCreating(ModelBuilder builder)
    //{
    //    base.OnModelCreating(builder);

    //    builder.Entity<District>()
    // .HasOne<State>() // District has one State
    // .WithMany() // State has many Districts
    // .HasForeignKey(d => d.StateId) // Foreign key in District referencing State
    // .OnDelete(DeleteBehavior.Cascade); // Cascade delete, if needed

    //    // Optionally, configure State entity's primary key explicitly
    //    builder.Entity<State>()
    //        .HasKey(s => s.Id); // Set Id as the primary key for State

    //    builder.Entity<District>()
    //        .HasKey(d => d.Id); // Set Id as the primary key for District
    //}
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure District and State Relationship
        builder.Entity<District>()
            .HasOne(d => d.State)
            .WithMany(s => s.Districts)
            .HasForeignKey(d => d.StateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<State>()
            .HasKey(s => s.Id);

        builder.Entity<District>()
            .HasKey(d => d.Id);

        builder.Entity<CustomerInfo>()
            .HasOne(c => c.District)
            .WithMany(d => d.CustomerInfos) // Optional: add reverse navigation
            .HasForeignKey(c => c.DistrictId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}
