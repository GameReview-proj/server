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
        try
        {
            await _service.CreateUser(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
}