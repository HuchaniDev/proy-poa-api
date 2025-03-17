using FluentValidation;
using Proy.Domain.Models.StrategicAxis;
using Proy.Domain.Repositories.StrategicAxis;

namespace Proy.Application.Validators.StrategicAxisService;

public class StrategicAxisValidator:AbstractValidator<StrategicAxisModel>
{
    int MaxDescriptionLength = 100;
    int MaxCodeLength = 20;
    
    private readonly IStrategicAxisRepository _strategicAxisRepository;
   public StrategicAxisValidator(
       IStrategicAxisRepository strategicAxisRepository
       )
   {
       _strategicAxisRepository = strategicAxisRepository;
       
       RuleFor(x => x.Description)
           .NotEmpty().WithMessage("La descripción es requerida")
           .MaximumLength(MaxDescriptionLength).WithMessage($"La descripción debe tener un máximo de {MaxDescriptionLength} caracteres");
       
       RuleFor(x=>x.Code)
           .NotEmpty().WithMessage("El código es requerido")
           .MustAsync(async (strategicAxis , code, cancellation) => await IsInUseCode(strategicAxis.Id,code, cancellation))
           .WithMessage("El código ya esta en uso");
   }

   private async Task<bool> IsInUseCode(int strategicAxisId ,int code, CancellationToken token)
   {
       var strategicAxis = await _strategicAxisRepository.GetByCodeAsync(code);
       return strategicAxis == null || strategicAxis.Id == strategicAxisId;
   }
}