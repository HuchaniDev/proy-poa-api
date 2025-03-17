using FluentValidation;
using Proy.Domain.Dtos.StrategicAxis;
using Proy.Domain.Repositories.StrategicAxis;

namespace Proy.Application.Validators.StrategicAxisService;

public class StrategicLineValidator:AbstractValidator<StrategicLineRequestDto>
{
    private readonly IStrategicAxisRepository _strategicAxisRepository;
    int MaxNameLength = 200;

    public StrategicLineValidator(IStrategicAxisRepository strategicAxisRepository)
    {
        _strategicAxisRepository = strategicAxisRepository;                       
                
        RuleFor(x => x.Id)
            .Must(id => id >= 0)
            .WithMessage("Id invalido");
        
        RuleFor(x=>x.Name)
            .NotEmpty().WithMessage("El nombre es requerido")
            .MaximumLength(MaxNameLength).WithMessage($"El nombre debe tener un máximo de {MaxNameLength} caracteres");
        
        RuleFor(x =>x.StrategicAxisId)
            .MustAsync(async (StrategicAxisId, cancellation) => await ExistStrategicAxis(StrategicAxisId, cancellation)) 
            .WithMessage("El eje estratégico no existe");
    }
    
    private async Task<bool> ExistStrategicAxis(int strategicAxisId, CancellationToken token)
    {
        return await _strategicAxisRepository.ExistsByIdAsync(strategicAxisId);
    }
    
}