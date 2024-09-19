using GameReview.Data.Adapters;
using GameReview.Data.DTOs.User;
using GameReview.Models;
using Microsoft.AspNetCore.Identity;

namespace GameReview.Services;

public class UserService(UserManager<User> userManager)
{
    private readonly UserManager<User> _userManager = userManager;

    public async Task<User> CreateUser(InUserDTO dto)
    {
        var newUser = UserAdapter.ToEntity(dto);

        IdentityResult result = await _userManager.CreateAsync(newUser, dto.Password);

        if (!result.Succeeded) throw new ApplicationException("Falha ao cadastrar usuário");

        return newUser;
    }
}