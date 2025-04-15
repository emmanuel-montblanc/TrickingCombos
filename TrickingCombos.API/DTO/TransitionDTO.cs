namespace TrickingCombos.API.DTO;

public class TransitionDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<StanceDto> Stances { get; set; } = [];
}
