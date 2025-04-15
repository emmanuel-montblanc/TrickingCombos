using System.ComponentModel.DataAnnotations;

namespace TrickingCombos.API.Models;

public class Transition
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public List<Stance> Stances => TransitionStances.Select(ts => ts.Stance).ToList();
    public List<Trick> Tricks => TrickTransitions.Select(tt => tt.Trick).ToList();

    // Database mapping
    public List<TransitionStanceLink> TransitionStances { get; set; } = [];
    public List<TrickTransitionLink> TrickTransitions { get; set; } = [];
}