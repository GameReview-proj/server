using GameReview.DTOs.Commentary;
using GameReview.Models;

namespace GameReview.Services;

public interface ICommentaryService : IWriteable<Commentary, InCommentaryDTO>,
    IReadable<Commentary>,
    IDeletable
{

}