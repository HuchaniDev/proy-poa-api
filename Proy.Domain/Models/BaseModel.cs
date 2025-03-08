namespace Proy.Domain.Models;

public class BaseModel
{
    public int Id { get; set; }
    
    protected BaseModel(int id)=>Id = id;
}