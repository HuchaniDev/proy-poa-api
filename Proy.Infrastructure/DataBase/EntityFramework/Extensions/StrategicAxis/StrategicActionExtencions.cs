using Proy.Domain.Dtos.StrategicAxis;
using Proy.Infrastructure.DataBase.EntityFramework.Entities.StrategicAxis;

namespace Proy.Infrastructure.DataBase.EntityFramework.Extensions.StrategicAxis;

public static class StrategicActionExtencions
{
    public static StrategicActionEntity ToEntity(this StrategicActionRequestDto dto)
    {
        return new StrategicActionEntity
        {
            Id = dto.Id,
            Name = dto.Name,
            StrategicLineId = dto.StrategicLineId
        };
    }
    
    public static StrategicActionRequestDto ToDto(this StrategicActionEntity entity)
    {
        return new StrategicActionRequestDto(
            entity.Id,
            entity.Name,
            entity.StrategicLineId
        );
    }
}