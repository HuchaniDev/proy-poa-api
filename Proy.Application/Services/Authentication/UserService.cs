using System.Net;
using Microsoft.AspNetCore.Identity;
using Proy.Domain.Models.Authentication;
using Proy.Domain.Repositories.Authentication;
using Proy.Domain.Responses;

namespace Proy.Application.Services.Authentication;

public class UserService
{
    private readonly IPasswordHasher<UserModel> _passwordHasher;
    private readonly IUserRepository _userRepository;

    public UserService(IPasswordHasher<UserModel> passwordHasher, IUserRepository userRepository)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }
    
    public async Task<Result<object>> CreateUserAsync(UserModel user)
    {
        // Encriptar la contraseña antes de guardarla
        user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);
        
        // Guardar el usuario con la contraseña encriptada
        var savedUser = (await _userRepository.SaveAsync(user))!=null;
        if (savedUser)
        {
            return Result<object>.Success(new { }, HttpStatusCode.Created);
        }
        return Result<object>.Failure(new List<string>(){"Error al guardar el usuario"}, HttpStatusCode.Accepted);
    }
}