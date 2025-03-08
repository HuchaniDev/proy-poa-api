using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proy.Infrastructure.DataBase.EntityFramework.Entities.Authentication;

[Table("UserRoles")]
public class UserRoleEntity: BaseEntity
{
    [Required]
    public int UserId { get; set; }
    public UserEntity User { get; set; } 

    [Required]
    public int RoleId { get; set; }
    public RoleEntity Role { get; set; } 
}