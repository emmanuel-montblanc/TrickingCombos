namespace TrickingCombos.API.Models;

public class TransitionRequest
{
    public string Name { get; set; }
    public List<Guid> StanceIds { get; set; }
}
