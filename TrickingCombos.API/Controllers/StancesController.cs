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
            Name = name
        };
        _context.Stances.Add(stance);
        _context.SaveChanges();

        return Ok(stance);
    }

    [HttpDelete("{stanceName}")]
    public IActionResult DeleteStance([FromRoute] string stanceName)
    {
        var stance = _context.Stances.FirstOrDefault(x => x.Name == stanceName);
        if (stance is null)
        {
            return NotFound("Could not find a stance with this name");
        }
        _context.Stances.Remove(stance);
        _context.SaveChanges();

        return Ok();
    }
}