using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proy.Infrastructure.DataBase.EntityFramework.Entities.StrategicAxis;

[Table("StrategicActions")]
public class StrategicActionEntity: BaseEntity,IIdentifiable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public int StrategicLineId { get; set; }
    
    [ForeignKey("StrategicLineId")]
    public StrategicLineEntity StrategicLine { get; set; }
}