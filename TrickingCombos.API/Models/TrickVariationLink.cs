namespace TrickingCombos.API.Models;

public class TrickVariationLink
{
    public required Guid TrickId { get; set; }
    public required Trick Trick { get; set; }

    public required Guid VariationId { get; set; }
    public required Variation Variation { get; set; }
}