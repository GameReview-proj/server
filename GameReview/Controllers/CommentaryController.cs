using GameReview.Data.DTOs.Commentary;
using GameReview.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GameReview.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentaryController(CommentaryService service) : ControllerBase
{
    private readonly CommentaryService _service = service;

    [HttpPost]
    public IActionResult PostCommentary([FromBody] InCommentaryDTO dto)
    {
        _service.CreateCommentary(dto);

        return Ok();
    }
}