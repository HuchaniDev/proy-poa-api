using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proy.Infrastructure.DataBase.EntityFramework.Entities.Authentication;

[Table("Roles")]
public class RoleEntity:BaseEntity,IIdentifiable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    
    public List<UserRoleEntity> UserRoles { get; set; }
    public List<RolePermissionEntity> RolePermissions { get; set; } 
}