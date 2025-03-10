using FluentValidation;
using Proy.Domain.Models.Authentication;
using Proy.Domain.Repositories.Authentication;

namespace Proy.Application.Validators;

public class UserValidator : AbstractValidator<UserModel>
{
    private const int MaxNameLength = 15;
    private const int MinNameLength = 4;
    private const int MinPasswordLength = 6;
    private readonly IUserRepository _userRepository;
    
    public UserValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        
        RuleFor(x => x.Id)
            .Must(id => id >= 0)
            .WithMessage("Id invalido");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Nombre de usuario es requerido")
            .MaximumLength(MaxNameLength).WithMessage($"Maximo {MaxNameLength} characters")
            .MinimumLength(MinNameLength).WithMessage($"Minimo {MinNameLength} characters")
            .MustAsync(async (user, username, cancellation) => await IsUniqueUsername(user.Id, username))
            .WithMessage("Nombre de usuario ya esta en uso");
            //.MustAsync(UniqueUsername).WithMessage("Nombre de usuario ya esta en uso");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email es requerido")
            .EmailAddress().WithMessage("Email no es valido")
            .MustAsync(async(user, email, cancellation) => await UniqueEmail(user.Id, email, cancellation))
            .WithMessage("Email ya existe");
            //.MustAsync(UniqueEmail).WithMessage("Email ya existe");
        
        RuleFor(x => x.PasswordHash)
            .NotEmpty().WithMessage("la contraseña es requerida")
            .MinimumLength(MinPasswordLength).WithMessage($"tamaño de contraseña minimo {MinPasswordLength} caracteres")
            .Matches(@"^\S+$").WithMessage("La contraseña no debe contener espacios");;
    }
    private async Task<bool> IsUniqueUsername(int userId, string username)
    {
        var existingUserId = await _userRepository.IsUsedUsername(username);
        return existingUserId == null || existingUserId == userId;
    }
    
    private async Task<bool> UniqueEmail(int userId, string email, CancellationToken token)
    {
        var existingUser = await _userRepository.GetByEmail(email);
        return existingUser == null || existingUser.Id == userId;
    }
    
}