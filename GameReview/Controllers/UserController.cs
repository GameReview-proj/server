using GameReview.Data.DTOs.User;
using GameReview.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameReview.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(UserService service) : ControllerBase
{
    private readonly UserService _service = service;

    [HttpPost]
    public async Task<IActionResult> PostUser(InUserDTO dto)
    {
        await _service.CreateUser(dto);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(InLoginDTO dto)
    {
        var token = await _service.Login(dto);

        return Ok(token);
    }
}