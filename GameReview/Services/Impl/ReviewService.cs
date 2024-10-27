using GameReview.Builders.Impl;
using GameReview.Data;
using GameReview.DTOs.Review;
using GameReview.Models;
using GameReview.Services.Exceptions;

namespace GameReview.Services.Impl;

public class ReviewService(DatabaseContext context, 
    ReviewBuilder builder,
    UserService userService) : IReviewService
{
    private readonly DatabaseContext _context = context;
    private readonly ReviewBuilder _builder = builder;
    private readonly UserService _userService = userService;

    public Review Create(InReviewDTO dto, string userId)
    {
        var userFound = _userService.GetById(userId);

        var newReview = _builder
            .SetDescription(dto.Description)
            .SetExternalId(dto.ExternalId)
            .SetStars(dto.Stars)
            .SetUser(userFound)
            .Build();

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