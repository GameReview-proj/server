﻿using GameReview.Data;
using GameReview.Data.Adapters;
using GameReview.Data.DTOs.Review;
using GameReview.Models;

namespace GameReview.Services;

public class ReviewService(DatabaseContext context)
{
    private readonly DatabaseContext _context = context;
    public Review CreateReview(InReviewDTO dto)
    {
        var userFound = _context
                .Users
                .FirstOrDefault(u => u.Id.Equals(dto.UserId))
        ?? throw new ApplicationException($"Erro ao buscar usuário com id: {dto.UserId}");

        Review newReview = ReviewAdapter.ToEntity(dto, userFound);

        _context.Add(newReview);
        _context.SaveChanges();

        return newReview;
    }

    public Review GetById(int id)
    {
        return _context
            .Reviews
            .FirstOrDefault(r => r.Id.Equals(id))
            ?? throw new ApplicationException($"Erro ao buscar review com id: {id}");
    }

    public IEnumerable<Review> GetByUserIdExternalId(string? userId, string? externalId)
    {
        var reviewsFound = _context.Reviews.Where(r =>
            (string.IsNullOrEmpty(userId) || r.User.Id == userId) &&
            (string.IsNullOrEmpty(externalId) || r.ExternalId == externalId));

        return [.. reviewsFound];

    }
}