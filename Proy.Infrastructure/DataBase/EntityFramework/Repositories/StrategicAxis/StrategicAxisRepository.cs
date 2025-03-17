using Proy.Domain.Models.StrategicAxis;
using Proy.Domain.Repositories.StrategicAxis;
using Proy.Infrastructure.DataBase.EntityFramework.Context;
using Proy.Infrastructure.DataBase.EntityFramework.Entities.StrategicAxis;
using Proy.Infrastructure.DataBase.EntityFramework.Extensions.StrategicAxis;
using Proy.Infrastructure.DataBase.EntityFramework.Repositories.Common;

namespace Proy.Infrastructure.DataBase.EntityFramework.Repositories.StrategicAxis;

public class StrategicAxisRepository:GenericRepository<StrategicAxisEntity>, IStrategicAxisRepository
{
    public StrategicAxisRepository(ProyDbContext dbContext) : base(dbContext) {}

    public async Task<StrategicAxisModel?> SaveAsync(StrategicAxisModel model)
    {
        try
        {
            if (model.Id == 0)
            {
                var newEntity = await base.SaveAsync(model.ToEntity());
                return newEntity.ToModel();
            }
            var entity = model.ToEntity();
            var updatedEntity = await UpdateAsync(entity);
            return updatedEntity.ToModel();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<StrategicAxisModel?> GetByIdAsync(int id)
    {
        var entity = await base.GetByIdAsync(id);
        if(entity == null) return null;
        
        return entity.ToModel();
    }

    public Task<List<StrategicAxisModel>> GetAllAsync()
    {
        var entities = _dbContext.StrategicAxis.Select(sax=>sax.ToModel()).ToList();
        return Task.FromResult(entities);
    }

    public Task<List<StrategicAxisModel>> GetByDescriptionAsync(string text)
    {
        var query = _dbContext.StrategicAxis.Where(sax => sax.Description.Contains(text));
        var entities = query.Select(sax => sax.ToModel()).ToList();
        return Task.FromResult(entities);
    }

    public async Task<StrategicAxisModel?> GetByCodeAsync(int code)
    {
        var strategicAxis = _dbContext.StrategicAxis.Where(sa=>sa.Code == code).FirstOrDefault();
        return strategicAxis?.ToModel();
    }
}