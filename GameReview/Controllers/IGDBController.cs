using GameReview.Services.Impl.IGDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using GameReview.DTOs.IGDB;

namespace GameReview.Controllers;

//[Authorize]
[Route("igdb")]
[ApiController]
public class IgdbController(IGDBService service) : ControllerBase
{
    private readonly IGDBService _service = service;

    [HttpGet("game")]
    public IActionResult GetGames([FromQuery] string? name,
        [FromQuery] List<string>? fields,
        [FromQuery] List<int>? genres,
        [FromQuery] List<int>? platforms,
        [FromQuery] int? from = 0,
        [FromQuery] int? take = 30)
    {
        var gamesFound = _service.GetGames(name, fields, from, take, platforms, genres);

        if (gamesFound.IsNullOrEmpty()) return NoContent();

        return Ok(new OutGamePageDTO(gamesFound.First(q => q.Name.Equals("Games")).Result,
            gamesFound.First(q => q.Name.Equals("Count")).Count));
    }

    [HttpGet("game/{id}")]
    public IActionResult GetGameById(int id, [FromQuery] List<string>? fields)
    {
        var gameFound = _service.GetGameById(id, fields);

        return Ok(gameFound);
    }

    [HttpGet("genre")]
    public IActionResult GetGenres([FromQuery] List<string>? fields)
    {
        var genresFound = _service.GetGenres(fields);

        if (genresFound.IsNullOrEmpty()) return NoContent();

        return Ok(genresFound);
    }

    [HttpGet("platform")]
    public IActionResult GetPlatforms([FromQuery] List<string>? fields, [FromQuery] string? name)
    {
        var platformsFound = _service.GetPlatforms(fields, name);

        if (platformsFound.IsNullOrEmpty()) return NoContent();

        return Ok(platformsFound);
    }
}