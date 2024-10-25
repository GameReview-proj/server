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

        _service.FollowUser(followerId, followedId);

        return Ok();
    }
}