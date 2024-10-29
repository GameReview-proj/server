using GameReview.DTOs.User;
using GameReview.Services.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameReview.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(UserService service, BlobService blobService) : ControllerBase
{
    private readonly UserService _service = service;
    private readonly BlobService _blobService = blobService;

    [HttpPost]
    public async Task<IActionResult> PostUser(InUserDTO dto)
    {
        await _service.Create(dto);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(InLoginDTO dto)
    {
        var token = await _service.Login(dto);

        return Ok(token);
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetUserInfo([FromQuery] string? userId)
    {
        var _userId = userId is null ? User.FindFirst(ClaimTypes.NameIdentifier)?.Value : userId;

        var userFound = _service.ExternalGetById(_userId);

        return Ok(new OutUserDTO(userFound));
    }

    [Authorize]
    [HttpPut]
    public IActionResult UpdateUser(InPutUserDTO dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        _service.Update(dto, userId);

        return Ok();
    }

    [Authorize]
    [HttpPut("user-profile-picture")]
    public IActionResult UploadFile([FromForm] IFormFile file)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        string url = _blobService.UploadFile(file).Result;

        _service.UpdateProfilePicture(url, userId);

        return Ok();
    }
}