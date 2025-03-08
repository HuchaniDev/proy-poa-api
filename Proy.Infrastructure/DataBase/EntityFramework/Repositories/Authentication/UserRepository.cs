using Proy.Domain.Models.Authentication;
using Proy.Domain.Repositories.Authentication;
using Proy.Infrastructure.DataBase.EntityFramework.Context;
using Proy.Infrastructure.DataBase.EntityFramework.Entities.Authentication;
using Proy.Infrastructure.DataBase.EntityFramework.Extensions.Authentication;
using Proy.Infrastructure.DataBase.EntityFramework.Repositories.Common;

namespace Proy.Infrastructure.DataBase.EntityFramework.Repositories.Authentication;

public class UserRepository: GenericRepository<UserEntity>,IUserRepository
{
    private readonly ProyDbContext _dbContext;
    
    public UserRepository(ProyDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<UserModel> SaveAsync(UserModel model)
    {
        if (model.Id == 0)
        {
            var newEntity = model.ToEntity();
            return Task.FromResult(base.SaveAsync(newEntity).Result.ToModel());
        }
        var entity = model.ToEntity();
        return Task.FromResult(UpdateAsync(entity).Result.ToModel());
    }

    public Task<UserModel?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}