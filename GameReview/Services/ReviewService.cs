using GameReview.Data;
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
        ?? throw new ApplicationException("Erro ao buscar usuário com id: " + dto.UserId);

        Review newReview = ReviewAdapter.ToEntity(dto, userFound);

        _context.Add(newReview);
        _context.SaveChanges();

        return newReview;
    }
}