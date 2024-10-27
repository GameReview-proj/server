using GameReview.Data.Builders.Impl;
using GameReview.Data.DTOs.Commentary;
using GameReview.Services.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace GameReview.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CommentaryController(CommentaryService service) : ControllerBase
{
    private readonly CommentaryService _service = service;

    [HttpPost]
    public IActionResult PostCommentary([FromBody] InCommentaryDTO dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        _service.Create(dto, userId);

        return Ok();
    }

    [HttpGet]
    public IActionResult GetByReviewIdExternalIdUserId([FromQuery] int? reviewId, [FromQuery] string? externalId, [FromQuery] string? userId)
    {
        var commentariesFound = _service.GetByReviewIdExternalIdUserId(reviewId, externalId, userId);

        if (commentariesFound.IsNullOrEmpty())
            return NoContent();

        return Ok(commentariesFound
            .Select(c => new OutCommentaryDTO(c.Id, c.Comment, c.CreatedDate))
            .ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var commentaryFound = _service.GetById(id);

        return Ok(commentaryFound);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteById(int id)
    {
        _service.Delete(id);

        return NoContent();
    }
}