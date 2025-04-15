using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrickingCombos.API.Data;
using TrickingCombos.API.Extensions;
using TrickingCombos.API.Models;

namespace TrickingCombos.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransitionsController(TricksDbContext context) : ControllerBase
{
    private readonly TricksDbContext _context = context;

    [HttpGet]
    public IActionResult GetAllTransitions()
    {
        var stancesDtos = _context.Transitions
            .Include(t => t.Stances)
            .Select(t => t.ToDto()).ToList();
        return Ok(stancesDtos);
    }

    [HttpPost]
    public IActionResult AddTransition([FromBody] TransitionRequest transitionRequest)
    {
        if (string.IsNullOrWhiteSpace(transitionRequest.Name))
        {
            return BadRequest("Transition name is required.");
        }

        if (transitionRequest.StanceIds == null || !transitionRequest.StanceIds.Any())
        {
            return BadRequest("At least one StanceId is required.");
        }

        var stances = _context.Stances
            .Where(s => transitionRequest.StanceIds.Contains(s.Id))
            .ToList();

        if (stances.Count != transitionRequest.StanceIds.Count)
        {
            return BadRequest("One or more provided StanceIds are invalid.");
        }

        var transition = new Transition
        {
            Id = Guid.NewGuid(),
            Name = transitionRequest.Name,
            Stances = stances
        };

        _context.Transitions.Add(transition);
        _context.SaveChanges();

        return Ok(transition.ToDto());
    }

    [HttpPut("{transitionId}")]
    public IActionResult EditTransition([FromRoute] Guid transitionId, [FromBody] TransitionRequest transitionRequest)
    {
        var transition = _context.Transitions
            .Include(t => t.Stances)
            .FirstOrDefault(x => x.Id == transitionId);

        if (transition is null)
        {
            return NotFound("Could not find a stance with this name");
        }

        transition.Name = transitionRequest.Name;
        transition.Stances = _context.Stances
            .Where(s => transitionRequest.StanceIds.Contains(s.Id))
            .ToList();

        _context.Transitions.Update(transition);
        _context.SaveChanges();

        return Ok(transition.ToDto());
    }

    [HttpDelete("{transitionId}")]
    public IActionResult DeleteStance([FromRoute] Guid transitionId)
    {
        var transition = _context.Transitions.FirstOrDefault(x => x.Id == transitionId);
        if (transition is null)
        {
            return NotFound("Could not find a stance with this name");
        }
        _context.Transitions.Remove(transition);
        _context.SaveChanges();

        return Ok();
    }
}
