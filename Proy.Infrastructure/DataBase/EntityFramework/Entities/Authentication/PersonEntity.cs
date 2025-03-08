using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proy.Infrastructure.DataBase.EntityFramework.Entities.Authentication;

[Table("Persons")]
public class PersonEntity:BaseEntity, IIdentifiable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    [Required]
    public string LastNameFather { get; set; }
    [Required]
    public string LastNameMother { get; set; }
    [Required]
    public string Dni { get; set; }
    public string? Address { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    
    public int? UserId { get; set; }
    public UserEntity? User { get; set; } = null!;
}