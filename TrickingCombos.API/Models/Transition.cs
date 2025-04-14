using System.ComponentModel.DataAnnotations;

namespace TrickingCombos.API.Models;

public class Transition
{
    [Key]
    public required string Name { get; set; }
    public List<Stance> Stances => TransitionStances.Select(ts => ts.Stance).ToList();
    public List<Trick> Tricks => TrickTransitions.Select(tt => tt.Trick).ToList();

    // Database mapping
    public required List<TransitionStanceLink> TransitionStances { get; set; }
    public required List<TrickTransitionLink> TrickTransitions { get; set; }
}