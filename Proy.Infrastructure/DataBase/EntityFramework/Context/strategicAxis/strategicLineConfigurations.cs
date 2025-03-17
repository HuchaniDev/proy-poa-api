using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proy.Infrastructure.DataBase.EntityFramework.Entities.StrategicAxis;

namespace Proy.Infrastructure.DataBase.EntityFramework.Context.strategicAxis;

public class strategicLineConfigurations:IEntityTypeConfiguration<StrategicLineEntity>
{
    public void Configure(EntityTypeBuilder<StrategicLineEntity> builder)
    {
        builder.HasOne(sl=>sl.StrategicAxis)
            .WithMany(sa=>sa.StrategicLines)
            .HasForeignKey(sl=>sl.StrategicAxisId)
            .OnDelete(DeleteBehavior.Restrict);
        
    }
}