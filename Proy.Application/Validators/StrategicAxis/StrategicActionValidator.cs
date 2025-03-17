using FluentValidation;
using Proy.Domain.Dtos.StrategicAxis;
using Proy.Domain.Repositories.StrategicAxis;

namespace Proy.Application.Validators.StrategicAxisService;

public class StrategicActionValidator:AbstractValidator<StrategicActionRequestDto>
{
    private const int MaxLength = 200;
    IStrategicLineRepository _strategicLineRepository;
    
    public StrategicActionValidator(IStrategicLineRepository strategicLineRepository)
    {
        _strategicLineRepository = strategicLineRepository;
        
        RuleFor(x => x.Id)
            .Must(id => id >= 0)
            .WithMessage("Id invalido");
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es requerido")
            .MaximumLength(MaxLength).WithMessage($"El nombre debe tener un máximo de {MaxLength} caracteres");
        
        RuleFor(x => x.StrategicLineId)
            .MustAsync(async (strategicLineId, cancellation) => await ExistStrategicLine(strategicLineId, cancellation))
            .WithMessage("La línea estratégica no existe");
    }
    
    private async Task<bool> ExistStrategicLine(int strategicLineId, CancellationToken token)
    {
        return await _strategicLineRepository.ExistsByIdAsync(strategicLineId);
    }
    
}