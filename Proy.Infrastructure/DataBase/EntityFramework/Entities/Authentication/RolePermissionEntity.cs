using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proy.Infrastructure.DataBase.EntityFramework.Entities.Authentication;

[Table("RolePermissions")]
public class RolePermissionEntity:BaseEntity
{
    [Required]
    public int RoleId { get; set; }
    public RoleEntity Role { get; set; } = null!;

    [Required]
    public int PermissionId { get; set; }
    public PermissionEntity Permission { get; set; } = null!;
}