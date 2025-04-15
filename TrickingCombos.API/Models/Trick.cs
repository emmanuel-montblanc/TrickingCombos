using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrickingCombos.API.Models;


public class Trick
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required Stance DefaultLandingStance { get; set; }
    public ICollection<Transition> Transitions { get; set; } = [];
    public ICollection<Variation> Variations { get; set; } = [];

    // Database mapping
    public required Guid DefaultLandingStanceId { get; set; }
}
