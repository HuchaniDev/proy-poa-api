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
        
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Nombre de usuario es requerido")
            .MaximumLength(MaxNameLength).WithMessage($"Maximo {MaxNameLength} characters")
            .MinimumLength(MinNameLength).WithMessage($"Minimo {MinNameLength} characters")
            .MustAsync(UniqueUsername).WithMessage("Nombre de usuario ya esta en uso");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email es requerido")
            .EmailAddress().WithMessage("Email no es valido")
            .MustAsync(UniqueEmail).WithMessage("Email ya existe");
        
        RuleFor(x => x.PasswordHash)
            .NotEmpty().WithMessage("la contraseña es requerida")
            .MinimumLength(MinPasswordLength).WithMessage($"tamaño de contraseña minimo {MinPasswordLength} caracteres");
    }
    
    private async Task<bool> UniqueEmail(string email, CancellationToken token)
    {
        return !await _userRepository.IsUsedEmail(email);
    }
    
    private async Task<bool> UniqueUsername(string username, CancellationToken token)
    {
        return !await _userRepository.IsUsedUsername(username);
    }
}