using TrickingCombos.API.DTO;
using TrickingCombos.API.Models;

namespace TrickingCombos.API.Extensions;

public static class StanceExtensions
{
    public static StanceDto ToDto(this Stance stance)
    {
        return new StanceDto
        {
            Id = stance.Id,
            Name = stance.Name
        };
    }
}