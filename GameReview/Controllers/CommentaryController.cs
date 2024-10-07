using GameReview.Data.Adapters;
using GameReview.Data.DTOs.Commentary;
using GameReview.Services;
using GameReview.Services.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
        _service.Create(dto);

        return Ok();
    }

    [HttpGet]
    public IActionResult GetByReviewIdExternalIdUserId([FromQuery] int? reviewId, [FromQuery] string? externalId, [FromQuery] string? userId)
    {
        var commentariesFound = _service.GetByReviewIdExternalIdUserId(reviewId, externalId, userId);

        if (commentariesFound.IsNullOrEmpty())
            return NoContent();

        return Ok(commentariesFound
            .Select(CommentaryAdapter.ToCommentaryDTO)
            .ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var commentaryFound = _service.GetById(id);

        return Ok(commentaryFound);
    }
}