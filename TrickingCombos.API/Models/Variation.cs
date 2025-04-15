using System.ComponentModel.DataAnnotations;

namespace TrickingCombos.API.Models;

public class Variation
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public List<Stance> Stances => VariationStances.Select(vs => vs.Stance).ToList();
    public List<Trick> Tricks => TrickVariations.Select(tv => tv.Trick).ToList();

    // Database mapping
    public required List<VariationStanceLink> VariationStances { get; set; }
    public required List<TrickVariationLink> TrickVariations { get; set; }
}
