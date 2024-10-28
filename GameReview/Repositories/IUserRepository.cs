using GameReview.Models;

namespace GameReview.Repositories;

public interface IUserRepository : IRepository<User>
{
    User? GetByEmail(string email);
}