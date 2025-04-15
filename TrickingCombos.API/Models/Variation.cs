using System.ComponentModel.DataAnnotations;

namespace TrickingCombos.API.Models;

public class Variation
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required Stance LandingStance { get; set; }
    public ICollection<Trick> Tricks { get; set; } = [];

    public required Guid LandingStanceId { get; set; }
}
