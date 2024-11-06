using GameReview.DTOs.Vote;
using GameReview.Models;

namespace GameReview.Services;

public interface IVoteService : IWriteable<Vote, InVoteDTO>,
    IDeletable
{

}