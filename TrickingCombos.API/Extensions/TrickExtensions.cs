using TrickingCombos.API.DTO;
using TrickingCombos.API.Models;

namespace TrickingCombos.API.Extensions;

public static class TrickExtensions
{
    public static TrickDTO ToDto(this Trick trick)
    {
        return new TrickDTO
        {
            Id = trick.Id,
            Name = trick.Name,
            DefaultLandingStance = trick.DefaultLandingStance.ToDto(),
            Transitions = trick.Transitions
                .Select(t => t.ToDto())
                .ToList(),
            Variations = trick.Variations
                .Select(v => v.ToDto())
                .ToList()
        };
    }
}
