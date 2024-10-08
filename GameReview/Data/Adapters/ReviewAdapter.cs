﻿using GameReview.Data.DTOs.Review;
using GameReview.Models;

namespace GameReview.Data.Adapters;

public static class ReviewAdapter
{
    public static Review ToEntity(InReviewDTO dto, User user)
    {
        return new()
        {
            Description = dto.Description,
            Stars = dto.Stars,
            ExternalId = dto.ExternalId,
            User = user,
            CreatedDate = DateTime.Now
        };
    }

    public static OutReviewDTO ToReviewDTO(Review review)
    {
        return new(review.Id,
            review.Stars,
            review.Description,
            review.ExternalId,
            review.CreatedDate
        );
    }

    public static OutReviewUserDTO ToReviewUserDTO(Review review)
    {
        return new(review.Id,
            review.Stars,
            review.Description,
            review.ExternalId,
            review.CreatedDate,
            UserAdapter.ToDTO(review.User)
        );
    }
}