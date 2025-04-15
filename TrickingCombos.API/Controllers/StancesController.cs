using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrickingCombos.API.Data;
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
        var stances = _context.Stances.ToList();
        return Ok(stances);
    }

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

        return Ok(stance);
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

        return Ok(stance);
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