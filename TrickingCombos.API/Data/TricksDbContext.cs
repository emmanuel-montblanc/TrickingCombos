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
        // Configure TransitionStanceLink
        modelBuilder.Entity<TransitionStanceLink>()
            .HasKey(ts => new { ts.TransitionId, ts.StanceId });

        modelBuilder.Entity<TransitionStanceLink>()
            .HasOne(ts => ts.Transition)
            .WithMany(t => t.TransitionStances)
            .HasForeignKey(ts => ts.TransitionId);

        modelBuilder.Entity<TransitionStanceLink>()
            .HasOne(ts => ts.Stance)
            .WithMany(s => s.TransitionStances)
            .HasForeignKey(ts => ts.StanceId);

        // Configure VariationStanceLink
        modelBuilder.Entity<VariationStanceLink>()
            .HasKey(vs => new { vs.VariationId, vs.StanceId });

        modelBuilder.Entity<VariationStanceLink>()
            .HasOne(vs => vs.Variation)
            .WithMany(v => v.VariationStances)
            .HasForeignKey(vs => vs.VariationId);

        modelBuilder.Entity<VariationStanceLink>()
            .HasOne(vs => vs.Stance)
            .WithMany(s => s.VariationStances)
            .HasForeignKey(vs => vs.StanceId);

        // Configure TrickTransitionLink
        modelBuilder.Entity<TrickTransitionLink>()
            .HasKey(tt => new { tt.TrickId, tt.TransitionId });

        modelBuilder.Entity<TrickTransitionLink>()
            .HasOne(tt => tt.Trick)
            .WithMany(t => t.TrickTransitionsLinks)
            .HasForeignKey(tt => tt.TrickId);

        modelBuilder.Entity<TrickTransitionLink>()
            .HasOne(tt => tt.Transition)
            .WithMany(t => t.TrickTransitions)
            .HasForeignKey(tt => tt.TransitionId);

        // Configure TrickVariationLink
        modelBuilder.Entity<TrickVariationLink>()
            .HasKey(tv => new { tv.TrickId, tv.VariationId });

        modelBuilder.Entity<TrickVariationLink>()
            .HasOne(tv => tv.Trick)
            .WithMany(t => t.TrickVariationsLinks)
            .HasForeignKey(tv => tv.TrickId);

        modelBuilder.Entity<TrickVariationLink>()
            .HasOne(tv => tv.Variation)
            .WithMany(v => v.TrickVariations)
            .HasForeignKey(tv => tv.VariationId);

        // Configure Trick's DefaultLandingStance relationship
        modelBuilder.Entity<Trick>()
            .HasOne(t => t.DefaultLandingStance)
            .WithMany()
            .HasForeignKey(t => t.DefaultLandingStanceId);
    }
}