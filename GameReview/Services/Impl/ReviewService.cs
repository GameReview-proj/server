using GameReview.Builders.Impl;
using GameReview.Data;
using GameReview.DTOs.Review;
using GameReview.Models;
using GameReview.Repositories.Impl;
using GameReview.Services.Exceptions;

namespace GameReview.Services.Impl;

public class ReviewService(ReviewRepository repository,
    ReviewBuilder builder,
    UserService userService) : IReviewService
{
    private readonly ReviewRepository _repository = repository;
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

        _repository.Add(newReview);

        return newReview;
    }

    public Review GetById(int id)
    {
        return _repository.GetById(id);
    }

    public IEnumerable<Review> GetByUserIdExternalId(string? userId, string? externalId, int from, int take)
    {
        var reviewsFound = _repository.GetByUserIdExternalId(userId, externalId, from, take);

        return [.. reviewsFound];
    }

    public IEnumerable<Review> GetNewsPage(int from, int take)
    {
        var reviewsFound = _repository.GetNewsPageable(from, take);

        return reviewsFound;
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }
}