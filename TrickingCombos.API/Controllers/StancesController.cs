using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrickingCombos.API.Data;
using TrickingCombos.API.Extensions;
using TrickingCombos.API.Models;

namespace TrickingCombos.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StancesController(TricksDbContext context) : ControllerBase
{
    private readonly TricksDbContext _context = context;

    [HttpGet]
    public IActionResult GetAllStances()
    {
        var stancesDtos = _context.Stances
            .Select(s => s.ToDto())
            .ToList();
        return Ok(stancesDtos);
    }

    [Authorize]
    [HttpPost]
    public IActionResult AddStance([FromBody] string name)
    {
        var stance = new Stance()
        {
            Id = Guid.NewGuid(),
            Name = name
        };
        _context.Stances.Add(stance);
        _context.SaveChanges();

        return Ok(stance.ToDto());
    }

    [HttpPut("{stanceId}")]
    public IActionResult EditStance([FromRoute] Guid stanceId, [FromBody] string newName)
    {
        var stance = _context.Stances.FirstOrDefault(x => x.Id == stanceId);
        if (stance is null)
        {
            return NotFound("Could not find a stance with this name");
        }
        stance.Name = newName;
        _context.Stances.Update(stance);
        _context.SaveChanges();

        return Ok(stance.ToDto());
    } 

    [HttpDelete("{StanceId}")]
    public IActionResult DeleteStance([FromRoute] Guid stanceId)
    {
        var stance = _context.Stances.FirstOrDefault(x => x.Id == stanceId);
        if (stance is null)
        {
            return NotFound("Could not find a stance with this name");
        }
        _context.Stances.Remove(stance);
        _context.SaveChanges();

        return Ok();
    }
}