using GameReview.Services.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameReview.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class FollowController(FollowService service) : ControllerBase
{
    private readonly FollowService _service = service;

    [HttpPost]
    public IActionResult FollowUser([FromQuery] string followedId)
    {
        var followerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (followerId is null) return Unauthorized();

        _service.FollowUser(followerId, followedId);

        return Ok();
    }

    [HttpDelete]
    public IActionResult UnfollowUser([FromQuery] string followedId)
    {
        var followerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (followerId is null) return Unauthorized();

        _service.UnfollowUser(followerId, followedId);

        return NoContent();
    }
}