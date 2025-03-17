using Proy.Domain.Models.StrategicAxis;
using Proy.Domain.Repositories.Common;

namespace Proy.Domain.Repositories.StrategicAxis;

public interface IStrategicAxisRepository:IGenericRepository<StrategicAxisModel>
{
    Task<List<StrategicAxisModel>>GetAllAsync();
    Task<List<StrategicAxisModel>>GetByDescriptionAsync(string text);
    
    Task<StrategicAxisModel?>GetByCodeAsync(int code);
}