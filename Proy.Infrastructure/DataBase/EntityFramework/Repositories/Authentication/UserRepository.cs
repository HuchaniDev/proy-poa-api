using Microsoft.EntityFrameworkCore;
using Proy.Domain.Dtos.Authentication;
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
            var password = _dbContext.Users.AsNoTracking().FirstOrDefault(x => x.Id == model.Id)?.PasswordHash;
            var entity = model.ToEntity();
            entity.PasswordHash = password;
            await UpdateAsync(entity);
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

    // public Task<bool> IsUsedEmail(string email)
    // {
    //     var isUsed = _dbContext.Users.Any(x => x.Email == email);
    //     return Task.FromResult(isUsed);
    // }
    //
    // public Task<bool> IsUsedUsername(string username)
    // {
    //     var isUsed = _dbContext.Users.Any(x => x.Username == username);
    //     return Task.FromResult(isUsed);
    // }
    public Task<List<UserDetailDto>> GetAllAsync()
    {
        var users = _dbContext.Users.Select(u => u.ToDetailDto()).ToList();
        return Task.FromResult(users);
    }

    public Task<UserModel?> GetByEmail(string email)
    {
        var user = _dbContext.Users.AsNoTracking().FirstOrDefault(x => x.Email == email);
        return Task.FromResult(user?.ToModel());
    }

    public Task<int> IsUsedUsername(string username)
    {
        var isUsed = _dbContext.Users.Count(u => u.Username == username);
        return Task.FromResult(isUsed);
    }

    public Task<UserRolesPermission?> GetPermissionsByUsername(string username)
    {
        var user = _dbContext.Users
            .AsNoTracking()
            .Select(u => new UserRolesPermission(
                u.Id,
                u.Username,
                u.PasswordHash,
                u.UserRoles.Select(ur => ur.Role.Name).ToList(),
                u.UserRoles
                    .SelectMany(ur => ur.Role.RolePermissions)
                    .Select(rp => rp.Permission.Name)
                    .Distinct()
                    .ToList()
            ))
            .FirstOrDefaultAsync();
        return user;
    }

    public Task<List<string>> GetRolesAsync(int UserId)
    {
        var roles = _dbContext.UserRoles
            .Where(x => x.UserId == UserId)
            .Select(x => x.Role.Name)
            .ToList();
        return Task.FromResult(roles);
    }

    public Task<bool> ChangeStatusActiveAsync(int id, bool status)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.Id == id);
        if (user == null) return Task.FromResult(false);
        
        user.IsActive = status;
        if(UpdateAsync(user)!=null) return Task.FromResult(true);
        
        return Task.FromResult(false);
    }

    public Task<bool> ChangePasswordAsync(int id, string newPassword)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.Id == id);
        if (user == null) return Task.FromResult(false);
        
        user.PasswordHash = newPassword;
        if(UpdateAsync(user)!=null) return Task.FromResult(true);
        
        return Task.FromResult(false);
    }
}