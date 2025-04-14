namespace TrickingCombos.API.Models;

public class TrickVariationLink
{
    public required string TrickName { get; set; }
    public required Trick Trick { get; set; }

    public required string VariationName { get; set; }
    public required Variation Variation { get; set; }
}