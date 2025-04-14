using Microsoft.EntityFrameworkCore;
using TrickingCombos.API.Models;

namespace TrickingCombos.API.Data;
public class TricksDbContext : DbContext
{
    public DbSet<Stance> Stances { get; set; } = null!;
    public DbSet<Transition> Transitions { get; set; } = null!;
    public DbSet<Variation> Variations { get; set; } = null!;
    public DbSet<Trick> Tricks { get; set; } = null!;

    public DbSet<TransitionStanceLink> TransitionStances { get; set; } = null!;
    public DbSet<VariationStanceLink> VariationStances { get; set; } = null!;
    public DbSet<TrickTransitionLink> TrickTransitions { get; set; } = null!;
    public DbSet<TrickVariationLink> TrickVariations { get; set; } = null!;

    public TricksDbContext(DbContextOptions<TricksDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Many-to-Many Composite Keys
        modelBuilder.Entity<TransitionStanceLink>()
            .HasKey(ts => new { ts.TransitionName, ts.StanceName });

        modelBuilder.Entity<VariationStanceLink>()
            .HasKey(vs => new { vs.VariationName, vs.StanceName });

        modelBuilder.Entity<TrickTransitionLink>()
            .HasKey(tt => new { tt.TrickName, tt.TransitionName });

        modelBuilder.Entity<TrickVariationLink>()
            .HasKey(tv => new { tv.TrickName, tv.VariationName });
    }
}