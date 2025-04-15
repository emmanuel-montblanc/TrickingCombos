namespace TrickingCombos.API.Models;

public class TrickTransitionLink
{
    public required Guid TrickId { get; set; }
    public required Trick Trick { get; set; }

    public required Guid TransitionId { get; set; }
    public required Transition Transition { get; set; }
}
