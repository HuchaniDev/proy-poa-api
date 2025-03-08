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

    public async Task<UserModel?> SaveAsync(UserModel model)
    {
        try
        {
            if (model.Id == 0)
            {
                var newEntity = await base.SaveAsync(model.ToEntity());
                return newEntity.ToModel();
            }
            var entity = await UpdateAsync(model.ToEntity());
            return entity.ToModel();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public Task<UserModel?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsUsedEmail(string email)
    {
        var isUsed = _dbContext.Users.Any(x => x.Email == email);
        return Task.FromResult(isUsed);
    }

    public Task<bool> IsUsedUsername(string username)
    {
        var isUsed = _dbContext.Users.Any(x => x.Username == username);
        return Task.FromResult(isUsed);
    }
}