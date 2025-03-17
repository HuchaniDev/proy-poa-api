using Proy.Domain.Dtos.StrategicAxis;
using Proy.Infrastructure.DataBase.EntityFramework.Entities.StrategicAxis;

namespace Proy.Infrastructure.DataBase.EntityFramework.Extensions.StrategicAxis;

public static class StrategicLineExtencions
{
    public static StrategicLineEntity ToEntity(this StrategicLineRequestDto dto)
    {
        return new StrategicLineEntity
        {
            Id = dto.Id,
            Name = dto.Name,
            StrategicAxisId = dto.StrategicAxisId
        };
    }
    
    public static StrategicLineRequestDto ToDto(this StrategicLineEntity entity)
    {
        return new StrategicLineRequestDto(
            entity.Id,
            entity.Name,
            entity.StrategicAxisId
        );
    }}