namespace TrickingCombos.API.Models;

public class TrickTransitionLink
{
    public required string TrickName { get; set; }
    public required Trick Trick { get; set; }

    public required string TransitionName { get; set; }
    public required Transition Transition { get; set; }
}
