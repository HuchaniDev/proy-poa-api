using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proy.Infrastructure.DataBase.EntityFramework.Entities.StrategicAxis;
[Table("StrategicAxis")]
public class StrategicAxisEntity:BaseEntity,IIdentifiable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public int Code { get; set; }
    
    public ICollection<StrategicLineEntity> StrategicLines { get; set; }
    
}