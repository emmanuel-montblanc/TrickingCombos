namespace TrickingCombos.API.DTO;

public class TrickDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public StanceDto DefaultLandingStance { get; set; }
    public List<TransitionDTO> Transitions { get; set; } = [];
    public List<VariationDTO> Variations { get; set; }
}
