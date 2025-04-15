namespace TrickingCombos.API.Models;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Stance
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }

    public ICollection<Transition> Transitions { get; set; } = [];
    public ICollection<Variation> Variations { get; set; } = [];
}