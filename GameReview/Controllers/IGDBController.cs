using GameReview.Services.Impl.IGDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GameReview.Controllers;

//[Authorize]
[Route("igdb")]
[ApiController]
public class IgdbController(IGDBService service) : ControllerBase
{
    private readonly IGDBService _service = service;

    [HttpGet("game")]
    public IActionResult GetGamesByName([FromQuery] string? name, [FromQuery] List<string>? fields, [FromQuery] int? from = 0, [FromQuery] int? take = 30)
    {
        var gamesFound = _service.GetGamesByName(name, fields, from, take);

        if (gamesFound.IsNullOrEmpty()) return NoContent();

        return Ok(gamesFound);
    }

    [HttpGet("genre")]
    public IActionResult GetGenres([FromQuery] List<string>? fields)
    {
        var genresFound = _service.GetGenres(fields);

        if (genresFound.IsNullOrEmpty()) return NoContent();

        return Ok(genresFound);
    }
}