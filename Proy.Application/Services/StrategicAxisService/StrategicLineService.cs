using System.Net;
using FluentValidation;
using Proy.Domain.Dtos.StrategicAxis;
using Proy.Domain.Repositories.StrategicAxis;
using Proy.Domain.Responses;

namespace Proy.Application.Services;

public class StrategicLineService
{
    private readonly IStrategicLineRepository _strategicLineRepository;
    private readonly IValidator<StrategicLineRequestDto> _validator;

    public StrategicLineService(IStrategicLineRepository strategicLineRepository,
        IValidator<StrategicLineRequestDto> validator)
    {
        _strategicLineRepository = strategicLineRepository;
        _validator = validator;
    }
    
    public async Task<Result<object>> Save(StrategicLineRequestDto model)
    {
        var strategicLineValid = await _validator.ValidateAsync(model);
        if (!strategicLineValid.IsValid)
            return Result<object>.Failure(strategicLineValid.Errors.Select(x => x.ErrorMessage).ToList(), HttpStatusCode.BadRequest);

        var isCreated = (await _strategicLineRepository.SaveAsync(model));
        if (isCreated!=null)
            return Result<object>.Success(new{}, HttpStatusCode.Created);
            
        return Result<object>.Failure(new List<string> { "Error al guardar la línea estratégica" }, HttpStatusCode.Accepted);
            
    } 

    public async Task<Result<List<StrategicLineRequestDto>>> GetAllByStrategicAxisId(int strategicAxisId)
    {
        var strategicLine = await _strategicLineRepository.GetAllByStrategicAxisId(strategicAxisId);
        return Result<List<StrategicLineRequestDto>>.Success(strategicLine, HttpStatusCode.OK);
    }

    public async Task<Result<StrategicLineRequestDto>> GetById(int id)
    {
        var strategicLine = await _strategicLineRepository.GetByIdAsync(id);
        if (strategicLine == null)
            return Result<StrategicLineRequestDto>.Failure(new List<string> { "Línea estratégica no encontrada." }, HttpStatusCode.NotFound);

        return Result<StrategicLineRequestDto>.Success(strategicLine, HttpStatusCode.OK);
    }
    
    public async Task<Result<object>> Delete(int id)
    {
        if (await _strategicLineRepository.DeleteHardAsync(id))
        {
            return Result<object>.Success(new { }, HttpStatusCode.OK);
        }
        return Result<object>.Failure(new List<string> { "Error al eliminar la línea estratégica" }, HttpStatusCode.Accepted);
    }
    
}
