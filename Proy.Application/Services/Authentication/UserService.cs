using System.Net;
using System.Text.RegularExpressions;
using FluentValidation;
using Proy.Application.Services.Security;
using Proy.Domain.Dtos.Authentication;
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
            if (user.Id == 0)
            {
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                // Encriptar la contraseña antes de guardarla
                user.PasswordHash = _passwordHasher.HashPassword(user.PasswordHash);
                stopwatch.Stop();
                Console.WriteLine($"Tiempo de hash: {stopwatch.ElapsedMilliseconds} ms");
                // Guardar el usuario con la contraseña encriptada
                if ((await _userRepository.SaveAsync(user)) != null)
                    return Result<object>.Success(new { }, HttpStatusCode.Created);
            }
            else
            {
                if ((await _userRepository.SaveAsync(user)) != null)
                    return Result<object>.Success(new { }, HttpStatusCode.OK);
            }
            return Result<object>.Failure(new List<string>(){"Error al guardar el usuario"}, HttpStatusCode.Accepted);
        }
        return Result<object>.Failure(validateUser.Errors.Select(x => x.ErrorMessage).ToList(), HttpStatusCode.BadRequest);
    }

    public async Task<Result<object>> ChangeStatusActive(int id, bool status)
    {
        var changed = await _userRepository.ChangeStatusActiveAsync(id, status);
        return changed ? Result<object>.Success(new { }, HttpStatusCode.OK) : Result<object>.Failure(new List<string>(), HttpStatusCode.Accepted);
    }

    public async Task<Result<object>> ChangePassword(int id, string password)
    {
        bool isValid = Regex.IsMatch(password, @"^\S{6,}$");
        if (isValid)
        {
            var passwordHash = _passwordHasher.HashPassword(password);
            var changed = await _userRepository.ChangePasswordAsync(id, passwordHash);
            return changed ? Result<object>.Success(new { }, HttpStatusCode.OK) : Result<object>.Failure(new List<string>(), HttpStatusCode.Accepted);
        }
        
        return Result<object>.Failure(new List<string>(){"La contraseña debe tener al menos 6 caracteres, sin espacios"}, HttpStatusCode.BadRequest);
    }
    
    public async Task<Result<List<UserDetailDto>>>GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return Result<List<UserDetailDto>>.Success(users, HttpStatusCode.OK);
    }
    
    public async Task<Result<object>> DeleteAsync(int id)
    {
        var deleted = await _userRepository.DeleteHardAsync(id);
        if (deleted)
        {
            return Result<object>.Success(new { }, HttpStatusCode.OK);
        }
        return Result<object>.Failure(new List<string>(){"Error al eliminar el usuario"}, HttpStatusCode.Accepted);
    }
}