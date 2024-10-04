﻿using GameReview.Services.IGDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GameReview.Controllers;

[Authorize]
[Route("igdb")]
[ApiController]
public class IgdbController(IGDBService service) : ControllerBase
{
    private readonly IGDBService _service = service;

    [HttpGet("game")]
    public IActionResult GetGamesByName([FromQuery] string? name)
    {
        var gamesFound = _service.GetGameByName(name);

        if (gamesFound.IsNullOrEmpty()) return NoContent();

        return Ok(gamesFound);
    }
}