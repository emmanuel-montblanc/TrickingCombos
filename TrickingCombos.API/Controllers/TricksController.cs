using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrickingCombos.API.Data;
using TrickingCombos.API.Extensions;
using TrickingCombos.API.Models;

namespace TrickingCombos.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TricksController(TricksDbContext context) : ControllerBase
{
    private readonly TricksDbContext _context = context;

    [HttpGet]
    public IActionResult GetAllTricks()
    {
        var tricksDtos = _context.Tricks
            .Include(t => t.Variations)
            .Include(t => t.Transitions)
            .Include(t => t.DefaultLandingStance)
            .Select(t => t.ToDto())
            .ToList();
        return Ok(tricksDtos);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult AddTricks([FromBody] TricksRequest tricksRequest)
    {
        if (string.IsNullOrWhiteSpace(tricksRequest.Name))
        {
            return BadRequest("Trick name is required.");
        }

        var defaultLandingStance = _context.Stances.FirstOrDefault(s => s.Id == tricksRequest.DefaultLandingStanceId);
        if (defaultLandingStance is null)
        {
            return BadRequest("The DefaultLandingStanceId is invalid.");
        }

        if (tricksRequest.TransitionIds is null || tricksRequest.TransitionIds.Count == 0)
        {
            return BadRequest("At least one TransitionId is required.");
        }

        var transitions = _context.Transitions
            .Where(t => tricksRequest.TransitionIds.Contains(t.Id))
            .ToList();

        if (transitions.Count != tricksRequest.TransitionIds.Count)
        {
            return BadRequest("One or more provided TransitionId are invalid.");
        }

        var variations = _context.Variations
            .Where(v => tricksRequest.VariationIds.Contains(v.Id))
            .ToList();

        if (variations.Count != tricksRequest.VariationIds.Count)
        {
            return BadRequest("One or more provided VariationId are invalid.");
        }

        var trick = new Trick
        {
            Id = Guid.NewGuid(),
            Name = tricksRequest.Name,
            DefaultLandingStanceId = tricksRequest.DefaultLandingStanceId,
            DefaultLandingStance = defaultLandingStance,
            Transitions = transitions,
            Variations = variations,
        };

        _context.Tricks.Add(trick);
        _context.SaveChanges();

        return Ok(trick.ToDto());
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{trickId}")]
    public IActionResult EditTrick([FromRoute] Guid trickId, [FromBody] TricksRequest tricksRequest)
    {
        var trick = _context.Tricks
            .Include(t => t.Variations)
            .Include(t => t.Transitions)
            .Include(t => t.DefaultLandingStance)
            .FirstOrDefault(x => x.Id == trickId);

        if (trick is null)
        {
            return NotFound("Could not find a trick with this id");
        }

        if (string.IsNullOrWhiteSpace(tricksRequest.Name))
        {
            return BadRequest("Trick name is required.");
        }

        var defaultLandingStance = _context.Stances.FirstOrDefault(s => s.Id == tricksRequest.DefaultLandingStanceId);
        if (defaultLandingStance is null)
        {
            return BadRequest("The DefaultLandingStanceId is invalid.");
        }

        if (tricksRequest.TransitionIds is null || tricksRequest.TransitionIds.Count == 0)
        {
            return BadRequest("At least one TransitionId is required.");
        }

        var transitions = _context.Transitions
            .Where(t => tricksRequest.TransitionIds.Contains(t.Id))
            .ToList();

        if (transitions.Count != tricksRequest.TransitionIds.Count)
        {
            return BadRequest("One or more provided TransitionId are invalid.");
        }

        var variations = _context.Variations
            .Where(v => tricksRequest.VariationIds.Contains(v.Id))
            .ToList();

        if (variations.Count != tricksRequest.VariationIds.Count)
        {
            return BadRequest("One or more provided VariationId are invalid.");
        }

        trick.Name = tricksRequest.Name;
        trick.DefaultLandingStanceId = tricksRequest.DefaultLandingStanceId;
        trick.DefaultLandingStance = defaultLandingStance;
        trick.Transitions = transitions;
        trick.Variations = variations;

        _context.Tricks.Update(trick);
        _context.SaveChanges();

        return Ok(trick.ToDto());
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{trickId}")]
    public IActionResult DeleteStance([FromRoute] Guid trickId)
    {
        var trick = _context.Tricks.FirstOrDefault(x => x.Id == trickId);
        if (trick is null)
        {
            return NotFound("Could not find a trick with this name");
        }
        _context.Tricks.Remove(trick);
        _context.SaveChanges();

        return Ok();
    }
}
