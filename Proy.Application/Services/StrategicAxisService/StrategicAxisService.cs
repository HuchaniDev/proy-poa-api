using System.Net;
using Proy.Domain.Models.StrategicAxis;
using Proy.Domain.Repositories.StrategicAxis;
using Proy.Domain.Responses;

namespace Proy.Application.Services;

public class StrategicAxisService
{
    private readonly IStrategicAxisRepository _strategicAxisRepository;

    public StrategicAxisService(IStrategicAxisRepository strategicAxisRepository)
    {
        _strategicAxisRepository = strategicAxisRepository;
    }
    
    public async Task<Result<object>> Save(StrategicAxisModel model)
    {
        var isCreated = (await _strategicAxisRepository.SaveAsync(model)) != null;
        return Result<object>.Success(new{}, HttpStatusCode.Created);
    }

    public async Task<Result<List<StrategicAxisModel>>> GetAll()
    {
        var strategicAxis = await _strategicAxisRepository.GetAllAsync();
        return Result<List<StrategicAxisModel>>.Success(strategicAxis, HttpStatusCode.OK);

    }
    
    public async Task<Result<StrategicAxisModel>> GetById(int id)
    {
        var strategicAxis = await _strategicAxisRepository.GetByIdAsync(id);
        if (strategicAxis == null)
        {
            return Result<StrategicAxisModel>.Failure(new List<string> { "Eje estratégico no encontrado." }, HttpStatusCode.NotFound);
        }
        return Result<StrategicAxisModel>.Success(strategicAxis, HttpStatusCode.OK);
    }
    
    public async Task<Result<List<StrategicAxisModel>>> GetByDescription(string text)
    {
        var strategicAxis = await _strategicAxisRepository.GetByDescriptionAsync(text);
        return Result<List<StrategicAxisModel>>.Success(strategicAxis, HttpStatusCode.OK);
    }
    
    public async Task<Result<bool>> Delete(int id)
    {
        if (await _strategicAxisRepository.DeleteHardAsync(id))
        {
            return Result<bool>.Success(default, HttpStatusCode.OK);
        }
        return Result<bool>.Failure(new List<string> { "No se pudo eliminar el eje estratégico." }, HttpStatusCode.BadRequest);
    }
}