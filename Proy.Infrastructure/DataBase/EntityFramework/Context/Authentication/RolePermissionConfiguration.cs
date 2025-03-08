using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proy.Infrastructure.DataBase.EntityFramework.Entities.Authentication;

namespace Proy.Infrastructure.DataBase.EntityFramework.Context.Authentication;

public class RolePermissionConfiguration:IEntityTypeConfiguration<RolePermissionEntity>
{
    public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
    {
        builder.HasKey(rp=>new{rp.RoleId,rp.PermissionId});
    }
}