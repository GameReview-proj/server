using GameReview.DTOs.User;
using GameReview.Models;

namespace GameReview.Services;

public interface IUserService : IDeletable
{
    public Task<string> Login(InLoginDTO dto);
    public User Update(InPutUserDTO dto, string id);
    public Task<User> Create(InUserDTO dto);
    public User UpdateProfilePicture(string pictureUrl, string id);
}