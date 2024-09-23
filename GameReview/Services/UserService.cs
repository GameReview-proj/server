using GameReview.Data;
using GameReview.Data.Adapters;
using GameReview.Data.DTOs.User;
using GameReview.Models;
using Microsoft.AspNetCore.Identity;

namespace GameReview.Services;

public class UserService(UserManager<User> userManager,
    SignInManager<User> signInManager,
    DatabaseContext context,
    TokenService tokenService)
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly DatabaseContext _context = context;
    private readonly TokenService _tokenService = tokenService;

    public async Task<User> CreateUser(InUserDTO dto)
    {
        var newUser = UserAdapter.ToEntity(dto);

        IdentityResult result = await _userManager.CreateAsync(newUser, dto.Password);

        if (!result.Succeeded) throw new ApplicationException("Falha ao cadastrar usuário");

        return newUser;
    }

    public async Task<string> Login(InLoginDTO dto)
    {
        var userFound = _signInManager
            .UserManager
            .Users
            .FirstOrDefault(u => u.UserName.Equals(dto.Login) || u.Email.Equals(dto.Login))
            ?? throw new ApplicationException("Usuário não encontrado");

        var result = await _signInManager.PasswordSignInAsync(userFound, dto.Password, false, false);

        if (!result.Succeeded) throw new ApplicationException("Falha ao fazer login");

        var token = _tokenService.GenerateToken(userFound);

        return token;
    }
}