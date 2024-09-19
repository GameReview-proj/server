using GameReview.Controllers.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GameReview.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController
{
    [HttpPost]
    public IActionResult PostUser([FromBody] InUserDTO dto)
    {


        return null;
    }
}