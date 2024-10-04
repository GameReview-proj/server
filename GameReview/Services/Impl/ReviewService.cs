using GameReview.Data;
using GameReview.Data.Adapters;
using GameReview.Data.DTOs.Review;
using GameReview.Models;
using GameReview.Services.Exceptions;

namespace GameReview.Services;

public class ReviewService(DatabaseContext context) : IReviewService
{
    private readonly DatabaseContext _context = context;
    public Review Create(InReviewDTO dto)
    {
        var userFound = _context
                .Users
                .FirstOrDefault(u => u.Id.Equals(dto.UserId))
        ?? throw new NotFoundException($"Usuário não encontrado com o id: {dto.UserId}");

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

    public IEnumerable<Review> GetByUserIdExternalId(string? userId, string? externalId)
    {
        var reviewsFound = _context.Reviews.Where(r =>
            (string.IsNullOrEmpty(userId) || r.User.Id == userId) &&
            (string.IsNullOrEmpty(externalId) || r.ExternalId == externalId));

        return [.. reviewsFound];
    }

    public void Delete(int id)
    {
        var reviewFound = _context.Reviews.FirstOrDefault(r =>
        r.Id.Equals(id))
            ?? throw new NotFoundException($"Review não encontrada com o id: {id}");

        reviewFound.Commentaries.Clear();
        _context.Reviews.Remove(reviewFound);
        _context.SaveChanges();
    }
}