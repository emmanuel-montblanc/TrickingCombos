namespace TrickingCombos.API.Models;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Stance
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }


    public List<Transition> Transitions => TransitionStances.Select(ts => ts.Transition).ToList();
    public List<Variation> Variations => VariationStances.Select(vs => vs.Variation).ToList();

    // Database mapping
    public List<TransitionStanceLink> TransitionStances { get; set; } = [];
    public List<VariationStanceLink> VariationStances { get; set; } = [];
}