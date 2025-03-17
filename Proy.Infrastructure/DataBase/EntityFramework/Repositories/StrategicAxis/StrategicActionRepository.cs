using Microsoft.EntityFrameworkCore;
using Proy.Domain.Dtos.StrategicAxis;
using Proy.Domain.Repositories.StrategicAxis;
using Proy.Infrastructure.DataBase.EntityFramework.Context;
using Proy.Infrastructure.DataBase.EntityFramework.Entities.StrategicAxis;
using Proy.Infrastructure.DataBase.EntityFramework.Extensions.StrategicAxis;
using Proy.Infrastructure.DataBase.EntityFramework.Repositories.Common;

namespace Proy.Infrastructure.DataBase.EntityFramework.Repositories.StrategicAxis;

public class StrategicActionRepository:GenericRepository<StrategicActionEntity>,IStrategicActionRepository
{
    public StrategicActionRepository(ProyDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<StrategicActionRequestDto?> SaveAsync(StrategicActionRequestDto dto)
    {
        try
        {
            if (dto.Id == 0)
            {
                var newEntity = await  base.SaveAsync(dto.ToEntity());
                return newEntity.ToDto();
            }
            var entity = dto.ToEntity();
            var updatedEntity = await UpdateAsync(entity);
            return updatedEntity.ToDto();
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<StrategicActionRequestDto?> GetByIdAsync(int id)
    {
        var entity = base.GetByIdAsync(id);
        return entity.Result?.ToDto();
    }

    public async Task<List<StrategicActionRequestDto>> GetAllByStrategicLineId(int strategicLineId)
    {
        var query = _dbContext.StrategicActions.AsNoTracking().Where(sax => sax.StrategicLineId == strategicLineId);
        return query.Select(sa => sa.ToDto()).ToList();
    }
}