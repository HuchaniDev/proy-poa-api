using Proy.Domain.Dtos.StrategicAxis;
using Proy.Domain.Repositories.Common;

namespace Proy.Domain.Repositories.StrategicAxis;

public interface IStrategicLineRepository:IGenericRepository<StrategicLineRequestDto>
{
    Task<List<StrategicLineRequestDto>>GetAllByStrategicAxisId(int strategicAxisId);
    
}