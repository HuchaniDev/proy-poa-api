using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Proy.Application.Services.Security;
using Proy.Domain.Models.Authentication;
using Proy.Domain.Repositories.Authentication;
using Proy.Domain.Responses;

namespace Proy.Application.Services.Authentication;

public class UserService
{
    //private readonly IPasswordHasher<UserModel> _passwordHasher;
    private readonly PasswordHasherService _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UserModel> _validator;

    public UserService(PasswordHasherService passwordHasher, IUserRepository userRepository, IValidator<UserModel> validator)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _validator = validator;
    }
    
    public async Task<Result<object>> SaveUserAsync(UserModel user)
    {
        var validateUser = await _validator.ValidateAsync(user);
        if (validateUser.IsValid)
        {
            // Encriptar la contraseña antes de guardarla
            user.PasswordHash = _passwordHasher.HashPassword(user.PasswordHash);
            
            // Guardar el usuario con la contraseña encriptada
            var savedUser = (await _userRepository.SaveAsync(user))!=null;
            if (savedUser)
            {
                return Result<object>.Success(new { }, HttpStatusCode.Created);
            }
            return Result<object>.Failure(new List<string>(){"Error al guardar el usuario"}, HttpStatusCode.Accepted);
        }
        return Result<object>.Failure(validateUser.Errors.Select(x => x.ErrorMessage).ToList(), HttpStatusCode.BadRequest);
    }
}