namespace TrickingCombos.API.Models;


public class VariationStanceLink
{
    public required Guid VariationId { get; set; }
    public required Variation Variation { get; set; }

    public required Guid StanceId { get; set; }
    public required Stance Stance { get; set; }
}
