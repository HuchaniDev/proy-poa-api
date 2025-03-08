using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proy.Infrastructure.DataBase.EntityFramework.Entities.Authentication;

namespace Proy.Infrastructure.DataBase.EntityFramework.Context.Authentication;

public class UserConfiguration: IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasOne(u=>u.Person)
            .WithOne(p=>p.User)
            .HasForeignKey<PersonEntity>(p=>p.UserId);
    }
}