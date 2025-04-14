namespace TrickingCombos.API.Models;


public class VariationStanceLink
{
    public required string VariationName { get; set; }
    public required Variation Variation { get; set; }

    public required string StanceName { get; set; }
    public required Stance Stance { get; set; }
}
