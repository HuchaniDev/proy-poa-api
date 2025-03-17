using Proy.Domain.Dtos.StrategicAxis;
using Proy.Domain.Repositories.Common;

namespace Proy.Domain.Repositories.StrategicAxis;

public interface IStrategicActionRepository:IGenericRepository<StrategicActionRequestDto>
{
    Task<List<StrategicActionRequestDto>>GetAllByStrategicLineId(int strategicLineId);
}