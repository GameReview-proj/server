using GameReview.Data;
using GameReview.Data.Adapters;
using GameReview.Data.DTOs.Commentary;
using GameReview.Models;

namespace GameReview.Services;

public class CommentaryService(DatabaseContext context)
{
    private readonly DatabaseContext _context = context;

    public Commentary CreateCommentary(InCommentaryDTO dto)
    {
        if ((dto.CommentaryId != null && dto.CommentaryId != 0) && (dto.ReviewId != null || dto.ReviewId == 0))
            throw new ApplicationException("Um comentário só pode ser atribuído a uma avaliação OU comentário");

        if ((dto.CommentaryId != null && dto.CommentaryId != 0) && !(_context
                .Commentaries
                .Any(r => r.Id.Equals(dto.CommentaryId))))
        {
            throw new ApplicationException($"Nenhum comentário encontrado com o id: {dto.CommentaryId}");
        }

        if ((dto.ReviewId != null || dto.ReviewId == 0) && !(_context
                .Reviews
                .Any(r => r.Id.Equals(dto.ReviewId))))
        {
            throw new ApplicationException($"Nenhum comentário encontrado com o id: {dto.CommentaryId}");
        }

        User user = _context
            .Users
            .FirstOrDefault(r => r.Id.Equals(dto.UserId))
        ?? throw new ApplicationException($"Usuário não encontrado com o id: {dto.UserId}");

        Review? reviewFound = _context
            .Reviews
            .FirstOrDefault(r => r.Id.Equals(dto.ReviewId));

        Commentary? commentaryFound = _context
            .Commentaries
            .FirstOrDefault(r => r.Id.Equals(dto.CommentaryId));

        Commentary newCommentary = CommentaryAdapter.ToEntity(dto, user, commentaryFound, reviewFound);

        _context.Commentaries.Add(newCommentary);
        _context.SaveChanges();

        return newCommentary;
    }
}