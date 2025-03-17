using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proy.Infrastructure.DataBase.EntityFramework.Entities.StrategicAxis;

namespace Proy.Infrastructure.DataBase.EntityFramework.Context.strategicAxis;

public class StrategicActionConfigurations:IEntityTypeConfiguration<StrategicActionEntity>
{
    public void Configure(EntityTypeBuilder<StrategicActionEntity> builder)
    {
        builder.HasOne(sa=>sa.StrategicLine)
            .WithMany(sl=>sl.StrategicActions)
            .HasForeignKey(sa=>sa.StrategicLineId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}