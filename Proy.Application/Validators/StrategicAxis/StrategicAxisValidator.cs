using FluentValidation;
using Proy.Domain.Models.StrategicAxis;

namespace Proy.Application.Validators.StrategicAxisService;

public class StrategicAxisValidator:AbstractValidator<StrategicAxisModel>
{
    int MaxDescriptionLength = 100;
    int MaxCodeLength = 20;
   public StrategicAxisValidator()
   {
       RuleFor(x => x.Description)
           .NotEmpty().WithMessage("La descripción es requerida")
           .MaximumLength(MaxDescriptionLength).WithMessage($"La descripción debe tener un máximo de {MaxDescriptionLength} caracteres");
       
       RuleFor(x=>x.Code)
           .NotEmpty().WithMessage("El código es requerido");
   } 
}