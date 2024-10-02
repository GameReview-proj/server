using GameReview.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameReview.Controllers;

[Authorize]
[Route("igdb")]
[ApiController]
public class IgdbController(IGDBTokenService service) : ControllerBase
{
    private readonly IGDBTokenService _service = service;

    [HttpGet("game")]
    public IActionResult GetGamesByName([FromQuery] string? name)
    {
        return Ok(_service.GenerateToken());
    }
}