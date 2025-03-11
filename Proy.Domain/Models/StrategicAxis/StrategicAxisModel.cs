namespace Proy.Domain.Models.StrategicAxis;

public class StrategicAxisModel:BaseModel
{
    public string Description { get; private set; }
    public int Code { get;private set;}

    public StrategicAxisModel(int id, string description, int code) : base(id)
    {
        Description = description;
        Code = code;
    }
}