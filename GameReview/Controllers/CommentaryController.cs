﻿using GameReview.Data.Adapters;
using GameReview.Data.DTOs.Commentary;
using GameReview.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

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
        _service.CreateCommentary(dto);

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
}