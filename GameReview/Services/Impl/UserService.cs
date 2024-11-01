using GameReview.Builders.Impl;
using GameReview.DTOs.User;
using GameReview.Models;
using GameReview.Repositories.Impl;
using GameReview.Services.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace GameReview.Services.Impl;

public class UserService(UserManager<User> userManager,
    SignInManager<User> signInManager,
    TokenService tokenService,
    UserBuilder builder,
    UserRepository repository) : IUserService
{
    private readonly UserRepository _repository = repository;
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly TokenService _tokenService = tokenService;
    private readonly UserBuilder _builder = builder;

    public async Task<User> Create(InUserDTO dto)
    {
        if (_repository.GetByEmail(dto.Email) is not null) throw new ConflictException($"Usuário já existente com o email: {dto.Email}");
        if (_repository.GetByUsername(dto.Username) is not null) throw new ConflictException($"Usuário já existente com o username: {dto.Username}");

        var newUser = _builder
            .SetEmail(dto.Email)
            .SetUsername(dto.Username)
            .Build();

        IdentityResult result = await _userManager.CreateAsync(newUser, dto.Password);

        if (!result.Succeeded) throw new ApplicationException("Falha ao cadastrar usuário");

        return newUser;
    }

    public void Delete(string id)
    {
        var userFound = ExternalGetById(id);

        _userManager.DeleteAsync(userFound);
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

    public User ExternalGetById(string id)
    {
        return _repository.GetById(id) ?? throw new NotFoundException($"Usuário não encontrado com o id: {id}");
    }

    public User Update(InPutUserDTO dto, string id)
    {
        var userFound = GetById(id);

        new UserBuilder(userFound)
            .SetEmail(dto.Email)
            .SetUsername(dto.Username);

        _repository.Update(userFound);

        return userFound;
    }

    public User UpdateProfilePicture(string pictureUrl, string id)
    {
        var userFound = GetById(id);

        userFound.Picture = pictureUrl;
        _repository.Update(userFound);

        return userFound;
    }

    public User GetById(string id)
    {
        return _repository.GetById(id);
    }
}