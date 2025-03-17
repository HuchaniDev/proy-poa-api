using System.Net;
using FluentValidation;
using Proy.Domain.Dtos.StrategicAxis;
using Proy.Domain.Repositories.StrategicAxis;
using Proy.Domain.Responses;

namespace Proy.Application.Services;

public class StrategicActionService
{
    private readonly IStrategicActionRepository _strategicActionRepository;
    private readonly IValidator<StrategicActionRequestDto> _validator;

    public StrategicActionService(IStrategicActionRepository strategicActionRepository, IValidator<StrategicActionRequestDto> validator)
    {
        _strategicActionRepository = strategicActionRepository;
        _validator = validator;
    }
    
    public async Task<Result<object>> Save(StrategicActionRequestDto dto)
    {
        var strategicActionValid = await _validator.ValidateAsync(dto);
        if (!strategicActionValid.IsValid)
            return Result<object>.Failure(strategicActionValid.Errors.Select(x => x.ErrorMessage).ToList(), HttpStatusCode.BadRequest);

        var isCreated = (await _strategicActionRepository.SaveAsync(dto));
        if (isCreated!=null)
            return Result<object>.Success(new{}, HttpStatusCode.Created);
       return Result<object>.Failure(new List<string>{"Error al guardar la acción estratégica"}, HttpStatusCode.BadRequest);
    }
    
    public async Task<Result<List<StrategicActionRequestDto>>> GetAllByStrategicLineId(int strategicLineId)
    {
        var strategicLine = await _strategicActionRepository.GetAllByStrategicLineId(strategicLineId);
        return Result<List<StrategicActionRequestDto>>.Success(strategicLine, HttpStatusCode.OK);
    }

    public async Task<Result<StrategicActionRequestDto>> GetById(int id)
    {
        var strategicAction = await _strategicActionRepository.GetByIdAsync(id);
        if (strategicAction == null)
            return Result<StrategicActionRequestDto>.Failure(new List<string> { "Acción estratégica no encontrada." }, HttpStatusCode.NotFound);
        
        return Result<StrategicActionRequestDto>.Success(strategicAction, HttpStatusCode.OK);
    }
    
    public async Task<Result<object>> Delete(int id)
    {
        if (await _strategicActionRepository.DeleteHardAsync(id))
        {
            return Result<object>.Success(new{}, HttpStatusCode.OK);
        }
        return Result<object>.Failure(new List<string>{"Error al eliminar la acción estratégica"}, HttpStatusCode.BadRequest);
    }
}