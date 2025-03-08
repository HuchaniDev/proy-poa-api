using Proy.Domain.Models.Authentication;
using Proy.Infrastructure.DataBase.EntityFramework.Entities.Authentication;

namespace Proy.Infrastructure.DataBase.EntityFramework.Extensions.Authentication;

public static class UserExtencion
{
    public static UserEntity ToEntity(this UserModel user)
    {
        return new UserEntity
        {
            Id = user.Id,
            Username = user.Username,
            PasswordHash = user.PasswordHash,
            Email = user.Email,
            IsActive = user.IsActive,
        };
    }
    
    public static UserModel? ToModel(this UserEntity user)
    {
        return new UserModel
        (
            user.Id,
            user.Username,
            user.PasswordHash,
            user.Email,
            user.IsActive
        );
    }
}