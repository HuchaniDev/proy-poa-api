using Proy.Domain.Dtos.StrategicAxis;
using Proy.Domain.Repositories.StrategicAxis;
using Proy.Infrastructure.DataBase.EntityFramework.Context;
using Proy.Infrastructure.DataBase.EntityFramework.Entities.StrategicAxis;
using Proy.Infrastructure.DataBase.EntityFramework.Extensions.StrategicAxis;
using Proy.Infrastructure.DataBase.EntityFramework.Repositories.Common;

namespace Proy.Infrastructure.DataBase.EntityFramework.Repositories.StrategicAxis;

public class StrategicLineRepository:GenericRepository<StrategicLineEntity>,IStrategicLineRepository
{
    protected StrategicLineRepository(ProyDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<StrategicLineRequestDto?> SaveAsync(StrategicLineRequestDto entity)
    {
        try
        {
            if (entity.Id == 0)
            {
                var newEntity = await base.SaveAsync(entity.ToEntity());
                return newEntity.ToDto();
            }
            var updatedEntity = await UpdateAsync(entity.ToEntity());
            return updatedEntity.ToDto();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public Task<StrategicLineRequestDto?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<StrategicLineRequestDto>> GetAllByStrategicAxisId(int strategicAxisId)
    {
        var query = _dbContext.StrategicLines.Where(sax => sax.StrategicAxisId == strategicAxisId);

        return query.Select(sax => sax.ToDto()).ToList();
    }
}