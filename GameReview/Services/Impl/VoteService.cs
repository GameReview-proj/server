using GameReview.Builders.Impl;
using GameReview.DTOs.Vote;
using GameReview.Models;
using GameReview.Repositories.Impl;
using GameReview.Services.Exceptions;

namespace GameReview.Services.Impl;

public class VoteService(VoteRepository repository,
    VoteBuilder builder,
    ReviewService reviewService,
    CommentaryService commentaryService,
    UserService userService) : IVoteService
{
    private readonly VoteRepository _repository = repository;
    private readonly VoteBuilder _builder = builder;
    private readonly ReviewService _reviewService = reviewService;
    private readonly CommentaryService _commentaryService = commentaryService;
    private readonly UserService _userService = userService;

    public Vote Create(InVoteDTO dto, string userId)
    {
        if (dto.ReviewId == null && dto.CommentaryId == null) throw new BadRequestException("Um voto deve ser atribuído a uma review OU comentário");
        if (dto.ReviewId != null && dto.CommentaryId != null) throw new BadRequestException("Um voto só pode ser atribuído a uma review OU um comentário");

        var voteFound = _repository.GetByReviewIdCommentaryId(dto.ReviewId, dto.CommentaryId, userId);

        if (voteFound != null)
        {
            if (voteFound.Up == dto.Up) throw new ConflictException($"Seu voto já foi registrado");
            else Delete(voteFound.Id);
        }

        var userFound = _userService.GetById(userId);

        var newVote = _builder
            .SetUp(dto.Up)
            .SetUser(userFound)
            .Build();

        if (dto.ReviewId != null)
        {
            var reviewFound = _reviewService.GetById((int)dto.ReviewId);

            newVote = _builder
                .ReBuild(newVote)
                .SetReview(reviewFound)
                .Build();
        }

        if (dto.CommentaryId != null)
        {
            var commentaryFound = _commentaryService.GetById((int)dto.CommentaryId);

            newVote = _builder
                .ReBuild(newVote)
                .SetCommentary(commentaryFound)
                .Build();
        }

        _repository.Add(newVote);

        return newVote;
    }
    public Vote GetByLinkIdUserId(int? reviewId, int? commentaryId, string userId)
    {
        return _repository.GetByReviewIdCommentaryId(reviewId, commentaryId, userId) ?? throw new NotFoundException($"Voto não encontrado");
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }
}