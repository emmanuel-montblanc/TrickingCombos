using System.ComponentModel.DataAnnotations;

namespace TrickingCombos.API.Models;

public class TransitionStanceLink
{
    public Guid TransitionId { get; set; }
    public Transition Transition { get; set; }

    public Guid StanceId { get; set; }
    public Stance Stance { get; set; }
}
