using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proy.Infrastructure.DataBase.EntityFramework.Entities.Authentication;

namespace Proy.Infrastructure.DataBase.EntityFramework.Context.Authentication;

public class UserRoleConfiguration: IEntityTypeConfiguration<UserRoleEntity>
{
    public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
    {
        builder.HasKey(ur=>new {ur.UserId, ur.RoleId});
    }
}