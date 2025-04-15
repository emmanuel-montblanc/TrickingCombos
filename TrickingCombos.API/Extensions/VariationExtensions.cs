using TrickingCombos.API.DTO;
using TrickingCombos.API.Models;

namespace TrickingCombos.API.Extensions;

public static class VariationExtensions
{
    public static VariationDTO ToDto(this Variation variation)
    {
        return new VariationDTO
        {
            Id = variation.Id,
            Name = variation.Name,
            LandingStance = variation.LandingStance
        };
    }  
}
