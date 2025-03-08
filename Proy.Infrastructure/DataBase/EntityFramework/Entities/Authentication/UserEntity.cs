using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proy.Infrastructure.DataBase.EntityFramework.Entities.Authentication;

[Table("Users")]
public class UserEntity:BaseEntity, IIdentifiable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string PasswordHash { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public bool IsActive { get; set; }
    
    public PersonEntity? Person { get; set; } = null!;
    public List<UserRoleEntity> UserRoles { get; set; } 
}