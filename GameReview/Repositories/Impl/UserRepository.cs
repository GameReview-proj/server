using GameReview.Data;
using GameReview.Models;

namespace GameReview.Repositories.Impl;

public class UserRepository(DatabaseContext context) : Repository<User>(context), IUserRepository
{
    public User? GetByEmail(string email)
    {
        return _dbSet.FirstOrDefault(u => u.Email == email);
    }
}