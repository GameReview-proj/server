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

    [HttpDelete]
    public IActionResult DeleteVote([FromQuery] int? reviewId, [FromQuery] int? commentaryId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var voteFound = _service.GetByLinkIdUserId(reviewId, commentaryId, userId);

        _service.Delete(voteFound.Id);

        return NoContent();
    }
}