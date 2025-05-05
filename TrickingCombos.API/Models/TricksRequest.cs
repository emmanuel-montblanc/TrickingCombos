namespace TrickingCombos.API.Models;

public class TricksRequest
{
    public string Name { get; set; }
    public Guid DefaultLandingStanceId { get; set; }
    public List<Guid> TransitionIds { get; set; }
    public List<Guid> VariationIds { get; set; }
}
