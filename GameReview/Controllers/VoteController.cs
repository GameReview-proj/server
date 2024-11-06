using GameReview.DTOs.Vote;
using GameReview.Services.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameReview.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class VoteController(VoteService service) : ControllerBase
{
    private readonly VoteService _service = service;

    [HttpPost]
    public IActionResult CreateVote([FromBody] InVoteDTO dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        _service.Create(dto, userId);

        return Ok();
    }
}