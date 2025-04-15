using System.ComponentModel.DataAnnotations;

namespace TrickingCombos.API.Models;

public class Transition
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public ICollection<Stance> Stances { get; set; } = [];
}