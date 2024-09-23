using GameReview.Data.DTOs.Review;
using GameReview.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameReview.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewController(ReviewService service) : ControllerBase
{
    private readonly ReviewService _service = service;

    [HttpPost]
    public IActionResult PostReview([FromBody] InReviewDTO dto)
    {
        _service.CreateReview(dto);

        return Ok();
    }
}