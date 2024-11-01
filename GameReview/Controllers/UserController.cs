using GameReview.DTOs.User;
using GameReview.Services.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using GameReview.Data.Caching.Impl;

namespace GameReview.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(UserService service, BlobService blobService, CachingService cacheService) : ControllerBase
{
    private readonly UserService _service = service;
    private readonly BlobService _blobService = blobService;
    private readonly CachingService _cacheService = cacheService;

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
        var effectiveUserId = userId ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(effectiveUserId)) return Unauthorized();

        var cachedUserDto = _cacheService.Get($"User:{effectiveUserId}");

        OutUserDTO? userDTO;

        if (cachedUserDto is not null)
        {
            userDTO = JsonSerializer.Deserialize<OutUserDTO>(cachedUserDto);

            return Ok(userDTO);
        }

        var userFound = _service.ExternalGetById(effectiveUserId);

        userDTO = new OutUserDTO(userFound);

        _cacheService.Set($"User:{effectiveUserId}", JsonSerializer.Serialize(userDTO));

        return Ok(userDTO);
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