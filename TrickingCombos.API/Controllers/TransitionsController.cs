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
            .Include(t => t.TransitionStances)
                .ThenInclude(ts => ts.Stance)
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
            TransitionStances = new List<TransitionStanceLink>()
        };

        foreach (var stance in stances)
        {
            var link = new TransitionStanceLink
            {
                TransitionId = transition.Id,
                Transition = transition,
                StanceId = stance.Id,
                Stance = stance
            };
            transition.TransitionStances.Add(link);
        }

        _context.Transitions.Add(transition);
        _context.SaveChanges();

        return Ok(transition.ToDto());
    }

    [HttpPut("{transitionId}")]
    public IActionResult EditTransition([FromRoute] Guid stanceId, [FromBody] TransitionRequest transitionRequest)
    {
        //var stance = _context.Stances.FirstOrDefault(x => x.Id == stanceId);
        //if (stance is null)
        //{
        //    return NotFound("Could not find a stance with this name");
        //}
        //stance.Name = newName;
        //_context.Stances.Update(stance);
        //_context.SaveChanges();

        //return Ok(stance);
        throw new NotImplementedException();
    }

    [HttpDelete("{StanceId}")]
    public IActionResult DeleteStance([FromRoute] Guid stanceId)
    {
        //var stance = _context.Stances.FirstOrDefault(x => x.Id == stanceId);
        //if (stance is null)
        //{
        //    return NotFound("Could not find a stance with this name");
        //}
        //_context.Stances.Remove(stance);
        //_context.SaveChanges();

        //return Ok();
        throw new NotImplementedException();
    }
}
