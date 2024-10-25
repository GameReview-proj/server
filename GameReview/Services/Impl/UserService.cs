using GameReview.Data;
using GameReview.Data.Adapters;
using GameReview.Data.DTOs.User;
using GameReview.Models;
using GameReview.Services.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace GameReview.Services.Impl;

public class UserService(UserManager<User> userManager,
    SignInManager<User> signInManager,
    DatabaseContext context,
    TokenService tokenService) : IUserService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly DatabaseContext _context = context;
    private readonly TokenService _tokenService = tokenService;

    public async Task<User> Create(InUserDTO dto)
    {
        if (_context.Users.Any(u => u.Email.Equals(dto.Email)))
            throw new ConflictException($"Usuário com email {dto.Email} já existente");

        var newUser = UserAdapter.ToEntity(dto);

        IdentityResult result = await _userManager.CreateAsync(newUser, dto.Password);

        if (!result.Succeeded) throw new ApplicationException("Falha ao cadastrar usuário");

        return newUser;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<string> Login(InLoginDTO dto)
    {
        var userFound = _signInManager
            .UserManager
            .Users
            .FirstOrDefault(u => u.UserName.Equals(dto.Login) || u.Email.Equals(dto.Login))
            ?? throw new NotFoundException("Usuário não encontrado");

        var result = await _signInManager.PasswordSignInAsync(userFound, dto.Password, false, false);

        if (!result.Succeeded) throw new ApplicationException("Falha ao fazer login");

        var token = _tokenService.GenerateToken(userFound);

        return token;
    }

    public User Update(InPutUserDTO dto, string id)
    {
        var userFound = _context.Users.FirstOrDefault(r => r.Id.Equals(id)) ?? throw new NotFoundException($"Usuário não encontrado com o id: {id}");

        UserAdapter.Update(dto, userFound);
        _context.SaveChanges();

        return userFound;
    }

    public User UpdateProfilePicture(string pictureUrl, string id)
    {
        var userFound = _context.Users.FirstOrDefault(r => r.Id.Equals(id)) ?? throw new NotFoundException($"Usuário não encontrado com o id: {id}");

        userFound.Picture = pictureUrl;
        _context.SaveChanges();

        return userFound;
    }

    // INTERNAL
    public User GetById(string id)
    {
        return _context.Users.FirstOrDefault(u => u.Id.Equals(id)) ?? throw new NotFoundException($"Usuário não encontrado com o id: {id}");
    }
}