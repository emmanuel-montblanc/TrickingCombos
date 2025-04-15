using System.ComponentModel.DataAnnotations;

namespace TrickingCombos.API.Models;

public class TransitionStanceLink
{
    public required Guid TransitionId { get; set; }
    public required Transition Transition { get; set; }

    public required Guid StanceId { get; set; }
    public required Stance Stance { get; set; }
}
