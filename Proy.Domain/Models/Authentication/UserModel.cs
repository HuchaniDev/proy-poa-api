using System.Text.Json.Serialization;

namespace Proy.Domain.Models.Authentication;

public class UserModel: BaseModel
{
    public string Username { get; private set; }
    public string PasswordHash { get; set; }
    public string Email { get; private set; }
    public bool IsActive { get; private set; }

    [JsonConstructor]
    public UserModel(int id, string username, string passwordHash, string email) : base(id)
    {
        Username = username;
        PasswordHash = passwordHash;
        Email = email;
        IsActive = true;
    }

    public UserModel(int id, string username, string passwordHash, string email, bool isActive) : base(id)
    {
        Username = username;
        PasswordHash = passwordHash;
        Email = email;
        IsActive = isActive;
    }
}