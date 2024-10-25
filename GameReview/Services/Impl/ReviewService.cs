using GameReview.Data;
using GameReview.Data.Adapters;
using GameReview.Data.DTOs.Review;
using GameReview.Models;
using GameReview.Services.Exceptions;

namespace GameReview.Services.Impl;

public class ReviewService(DatabaseContext context) : IReviewService
{
    private readonly DatabaseContext _context = context;
    public Review Create(InReviewDTO dto, string userId)
    {
        var userFound = _context
                .Users
                .FirstOrDefault(u => u.Id.Equals(userId))
        ?? throw new NotFoundException($"Usuário não encontrado com o id: {userId}");

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
            ?? throw new NotFoundException($"Review não encontrado com id: {id}");
    }

    public IEnumerable<Review> GetByUserIdExternalId(string? userId, string? externalId, int from, int take)
    {
        var reviewsFound = _context.Reviews.Where(r =>
            (string.IsNullOrEmpty(userId) || r.User.Id == userId) &&
            (string.IsNullOrEmpty(externalId) || r.ExternalId == externalId))
            .Skip(from)
            .Take(take);

        return [.. reviewsFound];
    }

    public IEnumerable<Review> GetNewsPage(int from, int take)
    {
        var reviewsFound = _context.Reviews
            .OrderBy(r => r.CreatedDate)
            .Skip(from)
            .Take(take);

        return reviewsFound;
    }

    public void Delete(int id)
    {
        var reviewFound = _context.Reviews.FirstOrDefault(r =>
        r.Id.Equals(id))
            ?? throw new NotFoundException($"Review não encontrada com o id: {id}");

        reviewFound?.Commentaries?.Clear();
        _context.Reviews.Remove(reviewFound);
        _context.SaveChanges();
    }
}