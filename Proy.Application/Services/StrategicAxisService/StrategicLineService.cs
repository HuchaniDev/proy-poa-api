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
        var strategicLineValid = _validator.Validate(model);
        if (!strategicLineValid.IsValid)
            return Result<object>.Failure(strategicLineValid.Errors.Select(x => x.ErrorMessage).ToList(), HttpStatusCode.BadRequest);

        var isCreated = (await _strategicLineRepository.SaveAsync(model)) != null;
        return Result<object>.Success(new{}, HttpStatusCode.Created);
    } 

    public async Task<Result<List<StrategicLineRequestDto>>> GetAllByStrategicAxisId(int strategicAxisId)
    {
        var strategicLine = await _strategicLineRepository.GetAllByStrategicAxisId(strategicAxisId);
        return Result<List<StrategicLineRequestDto>>.Success(strategicLine, HttpStatusCode.OK);
    }
    
    
}
