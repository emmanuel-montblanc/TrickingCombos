using System.ComponentModel.DataAnnotations;

namespace TrickingCombos.API.Models;

public class TransitionStanceLink
{
    public required string TransitionName { get; set; }
    public required Transition Transition { get; set; }

    public required string StanceName { get; set; }
    public required Stance Stance { get; set; }
}
