using GameReview.Builders.Impl;
using GameReview.Data;
using GameReview.DTOs.Commentary;
using GameReview.Models;
using GameReview.Repositories.Impl;
using GameReview.Services.Exceptions;

namespace GameReview.Services.Impl;

public class CommentaryService(CommentaryRepository repository,
    CommentaryBuilder builder,
    UserService userService,
    ReviewService reviewService) : ICommentaryService
{
    private readonly CommentaryRepository _repository = repository;
    private readonly CommentaryBuilder _builder = builder;
    private readonly UserService _userService = userService;
    private readonly ReviewService _reviewService = reviewService;

    public Commentary Create(InCommentaryDTO dto, string userId)
    {
        ValidateCommentary(dto);

        User user = _userService.GetById(userId);

        Commentary? commentaryFound = dto.CommentaryId != null && dto.CommentaryId != 0
            ? _repository.GetById(dto.CommentaryId)
            : null;

        Review? reviewFound = dto.ReviewId != null && dto.ReviewId != 0
            ? _reviewService.GetById((int)dto.ReviewId)
            : null;

        Commentary newCommentary = _builder
            .SetComment(dto.Comment)
            .SetLink(commentaryFound, reviewFound)
            .SetUser(user)
            .Build();

        _repository.Add(newCommentary);

        return newCommentary;
    }

    private static void ValidateCommentary(InCommentaryDTO dto)
    {
        bool hasCommentaryId = dto.CommentaryId != null && dto.CommentaryId != 0;
        bool hasReviewId = dto.ReviewId != null && dto.ReviewId != 0;

        if (hasCommentaryId && hasReviewId)
        {
            throw new BadRequestException("Um comentário só pode ser atribuído a uma avaliação OU a um comentário.");
        }
    }


    public IEnumerable<Commentary> GetByReviewIdExternalIdUserId(int? reviewId, string? externalId, string? userId)
    {
        var commentariesFound = _repository.GetByReviewIdExternalIdUserId(reviewId, externalId, userId);

        return [.. commentariesFound];
    }

    public Commentary GetById(int id)
    {
        Commentary commentaryFound = _repository.GetById(id);

        return commentaryFound;
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }
}