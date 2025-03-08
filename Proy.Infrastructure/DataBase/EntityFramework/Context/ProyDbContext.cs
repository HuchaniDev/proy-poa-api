using Microsoft.EntityFrameworkCore;
using Proy.Infrastructure.DataBase.EntityFramework.Context.Authentication;
using Proy.Infrastructure.DataBase.EntityFramework.Entities;
using Proy.Infrastructure.DataBase.EntityFramework.Entities.Authentication;

namespace Proy.Infrastructure.DataBase.EntityFramework.Context;

public class ProyDbContext:DbContext
{
    
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<PersonEntity> Persons { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<PermissionEntity> Permissions { get; set; }
    public DbSet<UserRoleEntity> UserRoles { get; set; }
    public DbSet<RolePermissionEntity> RolePermissions { get; set; }
    
    public ProyDbContext(DbContextOptions<ProyDbContext> options):base(options){}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new UserRoleConfiguration());
        builder.ApplyConfiguration(new RolePermissionConfiguration());
        
        base.OnModelCreating(builder);
    }

    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }
    
    private void UpdateAuditFields()
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = GetCurrentUserId();
                    entry.Entity.LastModifiedByAt = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy= GetCurrentUserId();
                    break;

                case EntityState.Modified:
                    entry.Property(nameof(BaseEntity.CreatedAt)).IsModified = false;
                    entry.Property(nameof(BaseEntity.CreatedBy)).IsModified = false;
                    entry.Entity.LastModifiedByAt = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = 104;
                    break;
            }
        }
    }
    
    private int GetCurrentUserId()
    {
        return 106;
    }
}