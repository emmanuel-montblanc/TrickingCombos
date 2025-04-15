using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrickingCombos.API.Data;
using TrickingCombos.API.Extensions;
using TrickingCombos.API.Models;

namespace TrickingCombos.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VariationsController(TricksDbContext context) : ControllerBase
{
    private readonly TricksDbContext _context = context;

    [HttpGet]
    public IActionResult GetAllVariations ()
    {
        var variationsDtos = _context.Variations
            .Include(v => v.LandingStance)
            .Select(v => v.ToDto()).ToList();
        return Ok(variationsDtos);
    }

    [HttpPost]
    public IActionResult AddTransition([FromBody] VariationRequest variationRequest)
    {
        if (string.IsNullOrWhiteSpace(variationRequest.Name))
        {
            return BadRequest("Variation name is required.");
        }

        var landingStance = _context.Stances.FirstOrDefault(s => s.Id == variationRequest.LandingStanceId);
        if (landingStance is null)
        {
            return BadRequest("The LandingStanceId is invalid.");
        }

        var variation = new Variation()
        {
            Id = Guid.NewGuid(),
            Name = variationRequest.Name,
            LandingStanceId = variationRequest.LandingStanceId,
            LandingStance = landingStance
        };

        _context.Variations.Add(variation);
        _context.SaveChanges();

        return Ok(variation.ToDto());
    }

    [HttpPut("{variationId}")]
    public IActionResult EditVariation([FromRoute] Guid variationId, [FromBody] VariationRequest variationRequest)
    {
        var variation = _context.Variations
            .Include(t => t.LandingStance)
            .FirstOrDefault(x => x.Id == variationId);

        if (variation is null)
        {
            return NotFound("Could not find a variation with this id");
        }

        var landingStance = _context.Stances.FirstOrDefault(s => s.Id == variationRequest.LandingStanceId);
        if (landingStance is null)
        {
            return BadRequest("The LandingStanceId is invalid.");
        }

        variation.Name = variationRequest.Name;
        variation.LandingStanceId = variationRequest.LandingStanceId;
        variation.LandingStance = landingStance;

        _context.Variations.Update(variation);
        _context.SaveChanges();

        return Ok(variation.ToDto());
    }

    [HttpDelete("{variationId}")]
    public IActionResult DeleteVariation([FromRoute] Guid variationId)
    {
        var variation = _context.Variations.FirstOrDefault(x => x.Id == variationId);
        if (variation is null)
        {
            return NotFound("Could not find a variation with this name");
        }
        _context.Variations.Remove(variation);
        _context.SaveChanges();

        return Ok();
    }
}
