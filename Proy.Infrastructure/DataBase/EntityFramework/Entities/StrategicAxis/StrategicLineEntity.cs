using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proy.Infrastructure.DataBase.EntityFramework.Entities.StrategicAxis;
[Table("StrategicLines")]
public class StrategicLineEntity:BaseEntity,IIdentifiable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    
    [Required]
    public int StrategicAxisId { get; set; }
    
    [ForeignKey("StrategicAxisId")]
    public StrategicAxisEntity StrategicAxis { get; set; }
    
    public ICollection<StrategicActionEntity> StrategicActions { get; set; }
}
