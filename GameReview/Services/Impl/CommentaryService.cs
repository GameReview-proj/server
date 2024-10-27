using GameReview.Data;
using GameReview.Data.Builders.Impl;
using GameReview.Data.DTOs.Commentary;
using GameReview.Models;
using GameReview.Services.Exceptions;

namespace GameReview.Services.Impl;

public class CommentaryService(DatabaseContext context,
    CommentaryBuilder builder,
    UserService userService) : ICommentaryService
{
    private readonly DatabaseContext _context = context;
    private readonly CommentaryBuilder _builder = builder;
    private readonly UserService _userService = userService;

    public Commentary Create(InCommentaryDTO dto, string userId)
    {
        CheckCommentary(dto);

        User user = _userService.GetById(userId);

        Review? reviewFound = _context
            .Reviews
            .FirstOrDefault(r => r.Id.Equals(dto.ReviewId));

        Commentary? commentaryFound = _context
            .Commentaries
            .FirstOrDefault(r => r.Id.Equals(dto.CommentaryId));

        Commentary newCommentary = _builder
            .SetComment(dto.Comment)
            .SetLink(commentaryFound, reviewFound)
            .SetUser(user)
            .Build();

        _context.Commentaries.Add(newCommentary);
        _context.SaveChanges();

        return newCommentary;
    }

    public IEnumerable<Commentary> GetByReviewIdExternalIdUserId(int? reviewId, string? externalId, string? userId)
    {
        var commentariesFound = _context
            .Commentaries
            .Where(r =>
                (!reviewId.HasValue || r.Review.Id.Equals(reviewId)) &&
                (string.IsNullOrEmpty(externalId) || r.Review.ExternalId.Equals(externalId) &&
                (string.IsNullOrEmpty(userId) || r.User.Id.Equals(userId)))
            );

        return [.. commentariesFound];
    }

    public Commentary GetById(int id)
    {
        Commentary commentaryFound = _context
            .Commentaries
            .FirstOrDefault(c => c.Id.Equals(id))
        ?? throw new NotFoundException($"Comentário não encontrado com o ID: {id}");

        return commentaryFound;
    }

    public void Delete(int id)
    {
        Commentary commentaryFound = GetById(id);

        _context
            .Commentaries
            .Remove(commentaryFound);
        _context.SaveChanges();
    }

    public void CheckCommentary(InCommentaryDTO dto)
    {
        if (dto.CommentaryId != null && dto.CommentaryId != 0 && (dto.ReviewId != null || dto.ReviewId == 0))
            throw new BadRequestException("Um comentário só pode ser atribuído a uma avaliação OU comentário");

        if (dto.CommentaryId != null && dto.CommentaryId != 0 && !_context
                .Commentaries
                .Any(r => r.Id.Equals(dto.CommentaryId)))
        {
            throw new NotFoundException($"Nenhum comentário encontrado com o id: {dto.CommentaryId}");
        }

        if ((dto.ReviewId != null || dto.ReviewId == 0) && !_context
                .Reviews
                .Any(r => r.Id.Equals(dto.ReviewId)))
        {
            throw new NotFoundException($"Nenhum comentário encontrado com o id: {dto.CommentaryId}");
        }
    }
}