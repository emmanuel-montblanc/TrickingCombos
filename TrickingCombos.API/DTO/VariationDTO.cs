using TrickingCombos.API.Models;

namespace TrickingCombos.API.DTO;

public class VariationDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Stance LandingStance { get; set; }
}
