using Microsoft.EntityFrameworkCore;
using TrickingCombos.API.Models;

namespace TrickingCombos.API.Data;
public class TricksDbContext : DbContext
{
    public DbSet<Stance> Stances { get; set; } = null!;
    public DbSet<Transition> Transitions { get; set; } = null!;
    public DbSet<Variation> Variations { get; set; } = null!;
    public DbSet<Trick> Tricks { get; set; } = null!;

    public TricksDbContext(DbContextOptions<TricksDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // many-to-many relationship between Transition and Stances
        modelBuilder.Entity<Transition>()
            .HasMany(t => t.Stances)
            .WithMany(s => s.Transitions);

        // many-to-many relationship between Trick and Variation
        modelBuilder.Entity<Trick>()
            .HasMany(t => t.Variations)
            .WithMany(v => v.Tricks);

        // Configure Variation's LandingStance relationship
        modelBuilder.Entity<Variation>()
            .HasOne(v => v.LandingStance)
            .WithMany()
            .HasForeignKey(t => t.LandingStanceId);

        // Configure Trick's DefaultLandingStance relationship
        modelBuilder.Entity<Trick>()
            .HasOne(t => t.DefaultLandingStance)
            .WithMany()
            .HasForeignKey(t => t.DefaultLandingStanceId);
    }
}