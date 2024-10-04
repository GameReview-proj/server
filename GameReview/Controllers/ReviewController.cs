﻿using GameReview.Data.Adapters;
using GameReview.Data.DTOs.Review;
using GameReview.Models;
using GameReview.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        _service.Create(dto);

        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetReviewById(int id)
    {
        var reviewFound = _service.GetById(id);

        return Ok(ReviewAdapter.ToReviewUserDTO(reviewFound));
    }

    [HttpGet]
    public IActionResult GetByUserIdExternalId([FromQuery] string? userId, [FromQuery] string? externalId)
    {
        var reviewsFound = _service.GetByUserIdExternalId(userId, externalId);

        var reviewsDTOs = reviewsFound.Select(ReviewAdapter.ToReviewDTO).ToList();

        return Ok(reviewsDTOs);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteById(int id)
    {
        _service.Delete(id);

        return NoContent();
    }
}