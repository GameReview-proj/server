using GameReview.Data;
using GameReview.Models;

namespace GameReview.Repositories.Impl;

public class FollowRepository(DatabaseContext context) : Repository<Follow>(context), IFollowRepository
{

}