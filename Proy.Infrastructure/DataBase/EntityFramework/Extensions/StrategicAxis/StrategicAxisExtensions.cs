using Proy.Domain.Models.StrategicAxis;
using Proy.Infrastructure.DataBase.EntityFramework.Entities.StrategicAxis;

namespace Proy.Infrastructure.DataBase.EntityFramework.Extensions.StrategicAxis;

public static class StrategicAxisExtensions
{
    public static StrategicAxisEntity ToEntity(this StrategicAxisModel model)
    {
        return new StrategicAxisEntity
        {
            Id = model.Id,
            Description = model.Description,
            Code = model.Code
        };
    }
    
    public static StrategicAxisModel ToModel(this StrategicAxisEntity entity)
    {
        return new StrategicAxisModel(entity.Id, entity.Description, entity.Code);
    }
}