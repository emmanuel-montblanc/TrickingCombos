using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrickingCombos.API.Models;


public class Trick
{
    [Key]
    public required string Name { get; set; }
    public required Stance DefaultLandingStance { get; set; }
    public List<Transition> Transitions => TrickTransitionsLinks.Select(tt => tt.Transition).ToList();
    public List<Variation> Variations => TrickVariationsLinks.Select(tv => tv.Variation).ToList();

    // Database mapping
    [ForeignKey("DefaultLandingStance")]
    public required string DefaultLandingStanceName { get; set; }
    public required List<TrickTransitionLink> TrickTransitionsLinks { get; set; }
    public required List<TrickVariationLink> TrickVariationsLinks { get; set; }
}
