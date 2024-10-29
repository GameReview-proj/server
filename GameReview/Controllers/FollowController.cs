using GameReview.DTOs.Follow;
using GameReview.DTOs.User;
using GameReview.Models;
using GameReview.Services.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

    [HttpGet("followers-following")]
    public IActionResult GetUserFollowersFollowings([FromQuery] string? userId)
    {
        var _userId = userId is null ? User.FindFirst(ClaimTypes.NameIdentifier)?.Value : userId;

        IEnumerable<Follow> followersFollows = _service.GetFollowers(_userId);
        IEnumerable<Follow> followingFollows = _service.GetFollowings(_userId);

        if (followersFollows.IsNullOrEmpty() && followingFollows.IsNullOrEmpty()) return NoContent();

        IEnumerable<OutUserDTO> followers = followersFollows.Select(f => new OutUserDTO(f.Follower));
        IEnumerable<OutUserDTO> following = followingFollows.Select(f => new OutUserDTO(f.Followed));

        return Ok(new OutFollowersFollowingDTO(followers, following));
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