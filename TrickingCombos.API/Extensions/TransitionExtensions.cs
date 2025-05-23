﻿using TrickingCombos.API.DTO;
using TrickingCombos.API.Models;

namespace TrickingCombos.API.Extensions;

public static class TransitionExtensions
{
    public static TransitionDTO ToDto(this Transition transition)
    {
        return new TransitionDTO
        {
            Id = transition.Id,
            Name = transition.Name,
            Stances = transition.Stances
                .Select(stance => stance.ToDto())
                .ToList()
        };
    }
}
