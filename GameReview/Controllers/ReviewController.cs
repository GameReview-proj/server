using GameReview.DTOs.Review;
using GameReview.DTOs.User;
using GameReview.Services.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace GameReview.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ReviewController(ReviewService service) : ControllerBase
{
    private readonly ReviewService _service = service;

    [HttpPost]
    public IActionResult PostReview([FromBody] InReviewDTO dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        _service.Create(dto, userId);

        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetReviewById(int id)
    {
        var reviewFound = _service.GetById(id);

        var reviewDTO = new OutReviewUserDTO(
                reviewFound.Id,
                reviewFound.Stars,
                reviewFound.Description,
                reviewFound.ExternalId,
                reviewFound.CreatedDate,
                new OutUserDTO(reviewFound.User)
        );

        return Ok(reviewDTO);
    }

    [HttpGet]
    public IActionResult GetByUserIdExternalId([FromQuery] string? userId, [FromQuery] string? externalId, int from = 0, int take = 20)
    {
        var reviewsFound = _service.GetByUserIdExternalId(userId, externalId, from, take);

        var reviewsDTOs = reviewsFound
            .Select(r => new OutReviewUserDTO(
                r.Id,
                r.Stars,
                r.Description,
                r.ExternalId,
                r.CreatedDate,
                new OutUserDTO(r.User)
            ))
            .ToList();

        return Ok(reviewsDTOs);
    }

    [HttpGet("news")]
    public IActionResult GetNewsPage([FromQuery] int from = 0, [FromQuery] int take = 20)
    {
        var reviewsFound = _service.GetNewsPage(from, take);

        if (reviewsFound.IsNullOrEmpty()) return NoContent();

        var reviewsDTOs = reviewsFound
            .Select(r => new OutReviewUserDTO(
                r.Id,
                r.Stars,
                r.Description,
                r.ExternalId,
                r.CreatedDate,
                new OutUserDTO(r.User)
            ))
            .ToList();

        return Ok(reviewsDTOs);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteById(int id)
    {
        _service.Delete(id);

        return NoContent();
    }
}